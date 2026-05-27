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

    public async Task AddOrUpdateStateAsync(string elementId, string json)
    {
        var elementGuid = Guid.Parse(elementId);
        _stateManager.AddOrUpdateState(elementGuid, json);
        await Clients.Others.SendAsync("ReceiveNewState", elementId, json);
    }

    public async Task DeleteElementAsync(string elementId)
    {
        var elementGuid = Guid.Parse(elementId);
        _stateManager.Remove(elementGuid);
        await Clients.Others.SendAsync("DeleteElement", elementId);
    }

    public async Task DeleteAllAsync()
    {
        _stateManager.RemoveAll();
        await Clients.Others.SendAsync("DeleteAll");
    }

    public async Task SaveElementPositionAsync(string elementId, string projectId)
    {
        var elementGuid = Guid.Parse(elementId);
        var projectGuid = Guid.Parse(projectId);
        var finalJson = _stateManager.GetElementState(elementGuid);

        if (finalJson != null)
        {
            var element = await _elementRepository.GetByIdAsync(elementGuid, Context.ConnectionAborted);

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
                    Id = elementGuid,
                    ProjectId = projectGuid,
                    Json = finalJson,
                    LastModified = DateTime.UtcNow
                };
                await _elementRepository.AddAsync(element, Context.ConnectionAborted);
            }
            await _elementRepository.SaveChangesAsync(Context.ConnectionAborted);
        }
    }
}