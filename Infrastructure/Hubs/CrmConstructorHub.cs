using Crm.Data.Entities;
using Crm.Data.Repositories.Interfaces;
using Crm.Logic.Layout;
using Microsoft.AspNetCore.SignalR;

namespace Crm.Infrastructure.Hubs;

public class CrmConstructorHub : Hub
{
    private readonly LayoutStateManager _stateManager;
    private readonly ICrmElementRepository _repository;

    public CrmConstructorHub(LayoutStateManager stateManager, ICrmElementRepository repository)
    {
        _stateManager = stateManager;
        _repository = repository;
    }


    public async Task UpdateState(Guid elementId, string json)
    {
        _stateManager.UpdateState(elementId, json);
        await Clients.Others.SendAsync("ReceiveNewState", elementId, json);
    }


    public async Task SaveElementPosition(Guid elementId)
    {
        var finalJson = _stateManager.GetElementState(elementId);

        if (finalJson != null)
        {
            var element = await _repository.GetByIdAsync(elementId, Context.ConnectionAborted);

            if (element != null)
            {

                element.Json = finalJson;
                element.LastModified = DateTime.UtcNow;

                _repository.Update(element);
            }
            else
            {
                element = new CrmElementEntity
                {
                    Id = elementId,
                    Json = finalJson,
                    LastModified = DateTime.UtcNow
                };
                await _repository.AddAsync(element, Context.ConnectionAborted);
            }
            await _repository.SaveChangesAsync(Context.ConnectionAborted);
        }
    }
}