using Microsoft.AspNetCore.SignalR;
using Crm.Data.Entities;
using Crm.Data.Repositories; 
using System;
using System.Threading.Tasks;

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

  
    public async Task MoveObject(Guid elementId, double x, double y)
    {
        _stateManager.UpdateElementState(elementId, x, y);
        await Clients.Others.SendAsync("ReceiveNewPosition", elementId, x, y);
    }

    
    public async Task SaveElementPosition(Guid elementId, Guid projectId)
    {
        var finalState = _stateManager.GetElementState(elementId);

        if (finalState != null)
        {
            var element = await _repository.GetByIdAsync(elementId, Context.ConnectionAborted);

            if (element != null)
            {
               
                element.X = finalState.X;
                element.Y = finalState.Y;
                element.LastModified = DateTime.UtcNow;

                element.ProjectId = projectId;

                _repository.Update(element);
            }
            else
            {
                
                element = new CrmElementEntity
                {
                    Id = elementId,
                    ProjectId = projectId,
                    X = finalState.X,
                    Y = finalState.Y,
                    LastModified = DateTime.UtcNow
                };
                await _repository.AddAsync(element, Context.ConnectionAborted);
            }

          
            await _repository.SaveChangesAsync(Context.ConnectionAborted);
        }
    }
}