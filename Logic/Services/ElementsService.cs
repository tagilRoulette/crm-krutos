using Crm.Data.Entities;
using Crm.Data.Repositories.Interfaces;
using Crm.Infrastructure.Hubs;
using Crm.Logic.Models;
using Crm.Logic.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Crm.Logic.Services;

public class ElementsService : IElementsService
{
    private readonly IElementsRepository _elementsRepository;
    private readonly IHubContext<CrmConstructorHub> _hub;

    public ElementsService(
        IElementsRepository elementsRepository,
        IHubContext<CrmConstructorHub> constructorHub)
    {
        _elementsRepository = elementsRepository;
        _hub = constructorHub;
    }

    public async Task<ElementModel> GetElementByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _elementsRepository.GetByIdAsync(id, cancellationToken);

        if (entity == null)
            throw new KeyNotFoundException($"Entity with ID {id} is not found.");

        return new(entity.Id, entity.Json, entity.LastModified, entity.PageId);
    }

    public async Task<IReadOnlyCollection<ElementModel>> GetAllElementsAsync(CancellationToken cancellationToken)
    {
        var entities = await _elementsRepository.GetAllAsync(cancellationToken);

        return [.. entities.Select(e => new ElementModel(e.Id, e.Json, e.LastModified, e.PageId))];
    }

    public async Task<IReadOnlyCollection<ElementModel>> GetElementsByPageIdAsync(Guid pageId, CancellationToken cancellationToken)
    {
        var entities = await _elementsRepository.GetByPageIdAsync(pageId, cancellationToken);
        return [..
            entities.Select(z => new ElementModel(z.Id, z.Json, z.LastModified, z.PageId))
            ];
    }

    public async Task<ElementModel> CreateElementAsync(Guid pageId, string? json, CancellationToken cancellationToken)
    {
        var entity = new ElementEntity
        {
            Id = Guid.NewGuid(),
            Json = json ?? "{}",
            LastModified = DateTime.UtcNow,
            PageId = pageId,
        };

        await _elementsRepository.AddAsync(entity, cancellationToken);
        await _elementsRepository.SaveChangesAsync(cancellationToken);

        await _hub.Clients.All.SendAsync("ReceiveNewState", entity.Id, entity.Json, cancellationToken);

        return new ElementModel(entity.Id, entity.Json, entity.LastModified, entity.PageId);
    }

    public async Task ChangeElementAsync(Guid id, string json, CancellationToken cancellationToken)
    {
        var entity = await _elementsRepository.GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            entity.Json = json;
            entity.LastModified = DateTime.UtcNow;


            _elementsRepository.Update(entity);
            await _elementsRepository.SaveChangesAsync(cancellationToken);


            await _hub.Clients.All.SendAsync("ReceiveNewState", id, json, cancellationToken);
        }
    }

    public async Task<bool> DeleteElementAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _elementsRepository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return false;


        await _elementsRepository.DeleteAsync(id, cancellationToken);
        await _elementsRepository.SaveChangesAsync(cancellationToken);


        await _hub.Clients.All.SendAsync("DeleteElement", id, cancellationToken);

        return true;
    }

    public async Task DeleteAllElementsAsync(CancellationToken cancellationToken)
    {
        await _elementsRepository.DeleteAllAsync(cancellationToken);

        await _elementsRepository.SaveChangesAsync(cancellationToken);

        await _hub.Clients.All.SendAsync("DeleteAll", cancellationToken);
    }

    public async Task<IReadOnlyCollection<ElementModel>> CreateElementsAsync(IEnumerable<Guid> pageIds, IReadOnlyList<string?> jsons, CancellationToken cancellationToken)
    {
        var entities = pageIds.Select((pageId, i) => new ElementEntity()
        {
            Id = Guid.NewGuid(),
            Json = jsons[i] ?? "{}",
            LastModified = DateTime.UtcNow,
            PageId = pageId,
        });

        foreach (var entity in entities)
        {
            await _elementsRepository.AddAsync(entity, cancellationToken);
            await _hub.Clients.All.SendAsync("ReceiveNewState", entity.Id, entity.Json, cancellationToken);
        }
        await _elementsRepository.SaveChangesAsync(cancellationToken);

        return [.. entities.Select(z => new ElementModel(z.Id, z.Json, z.LastModified, z.PageId))];
    }

    public async Task SaveOrUpdateElementAsync(Guid elementId, Guid pageId, string jsonState, CancellationToken cancellationToken)
    {
        var element = await _elementsRepository.GetByIdAsync(elementId, cancellationToken);
        if (element is null)
        {
            ElementEntity entity = new()
            {
                Id = elementId,
                Json = jsonState,
                PageId = pageId,
                LastModified = DateTime.UtcNow
            };
            await _elementsRepository.AddAsync(entity, cancellationToken);
        }
        else
        {
            element.Json = jsonState;
            _elementsRepository.Update(element);
        }
        await _elementsRepository.SaveChangesAsync(cancellationToken);
    }
}