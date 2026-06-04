using Crm.Data.Repositories.Interfaces;
using Crm.Logic.Layout;
using Crm.Logic.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Crm.Infrastructure.Hubs;

public class CrmConstructorHub : Hub
{
    private readonly LayoutStateManager _stateManager;
    private readonly IElementsService _elementsService;

    public CrmConstructorHub(
        LayoutStateManager stateManager,
        IElementsService elementService)
    {
        _stateManager = stateManager;
        _elementsService = elementService;
    }

    #region Elements
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

    public async Task DeleteAllElementsAsync()
    {
        _stateManager.RemoveAll();
        await Clients.Others.SendAsync("DeleteAllElements");
    }
    #endregion

    #region Pages
    public async Task CreatePageAsync(string pageId, string name)
    {
        await Clients.Others.SendAsync("CreatePage", pageId, name);
    }

    public async Task RenamePageAsync(string pageId, string newName)
    {
        await Clients.Others.SendAsync("RenamePage", pageId, newName);
    }

    public async Task DeletePageAsync(string pageId)
    {
        await Clients.Others.SendAsync("DeletePage", pageId);
    }

    public async Task DeleteAllPages()
    {
        await Clients.Others.SendAsync("DeleteAllPages");
    }
    #endregion

    public async Task SaveElementPositionAsync(string elementId, string pageId, string jsonState)
    {
        if (!Guid.TryParse(elementId, out var parsedElementId) ||
            !Guid.TryParse(pageId, out var parsedPageId))
        {
            throw new FormatException("Unrecognized Guid format. Expected standard UUID.");
        }

        // TODO
        // Сохраняем в БД через сервис элементов
        await _elementsService.SaveOrUpdateElementAsync(parsedElementId, parsedPageId, jsonState, Context.ConnectionAborted);

        // Рассылаем всем остальным подключенным клиентам, минуя групповую синхронизацию
        await Clients.Others.SendAsync("ElementPositionUpdated", elementId, pageId, jsonState);
    }
}