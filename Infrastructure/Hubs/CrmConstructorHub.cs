using Crm.Data.Entities;
using Crm.Data.Repositories.Interfaces;
using Crm.Logic.Layout;
using Microsoft.AspNetCore.SignalR;

namespace Crm.Infrastructure.Hubs;

public class CrmConstructorHub : Hub
{
    private readonly LayoutStateManager _stateManager;
    private readonly ICrmElementRepository _elementRepository;

    public CrmConstructorHub(
        LayoutStateManager stateManager,
        ICrmElementRepository elementRepository,
        IProjectsRepository projectRepository)
    {
        _stateManager = stateManager;
        _elementRepository = elementRepository;
    }

    public async Task AddOrUpdateStateAsync(Guid elementId, string json, CancellationToken cancellationToken)
    {
        _stateManager.AddOrUpdateState(elementId, json);
        await Clients.Others.SendAsync("ReceiveNewState", elementId, json, cancellationToken);
    }

    public async Task DeleteElementAsync(Guid elementId, CancellationToken cancellationToken)
    {
        _stateManager.Remove(elementId);
        await Clients.Others.SendAsync("DeleteElement", elementId, cancellationToken);
    }

    public async Task DeleteAllAsync(CancellationToken cancellationToken)
    {
        _stateManager.RemoveAll();
        await Clients.Others.SendAsync("DeleteAll", cancellationToken);
    }

    public async Task SaveElementPositionAsync(Guid elementId, CancellationToken cancellationToken)
    {
        var finalJson = _stateManager.GetElementState(elementId);

        if (finalJson != null)
        {
            var element = await _elementRepository.GetByIdAsync(elementId, Context.ConnectionAborted);

            if (element != null)
            {

                element.Json = finalJson;
                element.LastModified = DateTime.UtcNow;

                _elementRepository.Update(element);
            }
            else
            {
                element = new CrmElementEntity
                {
                    Id = elementId,
                    Json = finalJson,
                    LastModified = DateTime.UtcNow
                };
                await _elementRepository.AddAsync(element, Context.ConnectionAborted);
            }
            await _elementRepository.SaveChangesAsync(Context.ConnectionAborted);
        }
    }
}