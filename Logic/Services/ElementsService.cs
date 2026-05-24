using Crm.Data.Entities;
using Crm.Data.Repositories.Interfaces;
using Crm.Infrastructure.Hubs;
using Crm.Logic.Models;
using Crm.Logic.Services.Interfaces;
using Microsoft.AspNet.SignalR;

namespace Crm.Logic.Services;

public class ElementsService : IElementsService
{
    private readonly ICrmElementRepository _elementsRepository;
    private readonly IProjectsRepository _projectsRepository;
    private readonly IHubContext<CrmConstructorHub> _hub;

    public ElementsService(
        ICrmElementRepository elementsRepository,
        IProjectsRepository projectsRepository,
        IHubContext<CrmConstructorHub> constructorHub)
    {
        _elementsRepository = elementsRepository;
        _projectsRepository = projectsRepository;
        _hub = constructorHub;
    }

    public async Task<ElementModel> GetElementByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _elementsRepository.GetByIdAsync(id, cancellationToken);

        if (entity == null)
            throw new Exception($"Entity with ID {id} is not found.");

        return new ElementModel
        {
            Id = entity.Id,
            Json = entity.Json,
            LastModified = entity.LastModified
        };
    }

    public async Task<IReadOnlyCollection<ElementModel>> GetAllElementsAsync(CancellationToken cancellationToken)
    {
        var entities = await _elementsRepository.GetAllAsync(cancellationToken);

        return entities.Select(e => new ElementModel
        {
            Id = e.Id,
            Json = e.Json,
            LastModified = e.LastModified
        }).ToList();
    }

    public async Task<IReadOnlyCollection<ElementModel>> GetElementsByProjectIdAsync(Guid projectId, CancellationToken cancellationToken)
    {
        var project = await _projectsRepository.GetProjectByIdAsync(projectId, cancellationToken);
        if (project == null || project.Elements == null || !project.Elements.Any())
            return new List<ElementModel>();

        return project.Elements.Select(e => new ElementModel
        {
            Id = e.Id,
            Json = e.Json,
            LastModified = e.LastModified
        }).ToList();
    }

    public async Task<ElementModel> CreateElementAsync(Guid projectId, string? json, CancellationToken cancellationToken)
    {
        var entity = new CrmElementEntity
        {
            Id = Guid.NewGuid(),
            Json = json ?? "{}",
            LastModified = DateTime.UtcNow,
            ProjectId = projectId,
        };

        await _hub.AddOrUpdateStateAsync(entity.Id, entity.Json, cancellationToken);
        await _hub.SaveElementPositionAsync(entity.Id, cancellationToken);

        return new ElementModel
        {
            Id = entity.Id,
            Json = entity.Json,
            LastModified = entity.LastModified
        };
    }

    public async Task ChangeElementAsync(Guid id, string json, CancellationToken cancellationToken)
    {
        var entity = await _elementsRepository.GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            entity.Json = json;
            entity.LastModified = DateTime.UtcNow;

            await _hub.AddOrUpdateStateAsync(id, json, cancellationToken);
            await _hub.SaveElementPositionAsync(id, cancellationToken);

            //_elementsRepository.Update(entity);
            //await _elementsRepository.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<bool> DeleteElementAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _elementsRepository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            return false;

        await _hub.DeleteElementAsync(id, cancellationToken);
        await _elementsRepository.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task DeleteAllElementsAsync(CancellationToken cancellationToken)
    {
        await _hub.DeleteAllAsync(cancellationToken);
        await _elementsRepository.SaveChangesAsync(cancellationToken);
    }
}