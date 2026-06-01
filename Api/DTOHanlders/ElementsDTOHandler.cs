using Crm.Api.Controllers.Elements.DTO.Response;
using Crm.Api.DTOHanlders.Interfaces;
using Crm.Logic.Services.Interfaces;

namespace Crm.Api.DTOHanlders;

public class ElementsDTOHandler : IElementsDTOHandler
{
    private readonly IElementsService _elementsService;

    public ElementsDTOHandler(IElementsService elementsService)
    {
        _elementsService = elementsService;
    }

    public async Task<ElementResponse> GetElementByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var model = await _elementsService.GetElementByIdAsync(id, cancellationToken);
        return MapToResponse(model);
    }

    public async Task<ElementListResponse> GetAllElementsAsync(CancellationToken cancellationToken)
    {
        var models = await _elementsService.GetAllElementsAsync(cancellationToken);

        var responses = models.Select(MapToResponse).ToList();
        return new ElementListResponse(responses);
    }

    public async Task<ElementListResponse> GetElementsByProjectIdAsync(Guid projectId, CancellationToken cancellationToken)
    {
        var models = await _elementsService.GetElementsByPageIdAsync(projectId, cancellationToken);

        var responses = models.Select(MapToResponse).ToList();
        return new ElementListResponse(responses);
    }

    public async Task<ElementResponse> CreateElementAsync(Guid projectId, string? json, CancellationToken cancellationToken)
    {
        var model = await _elementsService.CreateElementAsync(projectId, json, cancellationToken);
        return MapToResponse(model);
    }

    public async Task ChangeElementAsync(Guid id, string json, CancellationToken cancellationToken)
    {
        await _elementsService.ChangeElementAsync(id, json, cancellationToken);
    }

    public async Task<bool> DeleteElementAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _elementsService.DeleteElementAsync(id, cancellationToken);
    }

    public async Task DeleteAllElementsAsync(CancellationToken cancellationToken)
    {
        await _elementsService.DeleteAllElementsAsync(cancellationToken);
    }

    // Вспомогательный метод для маппинга, чтобы не дублировать код
    private static ElementResponse MapToResponse(Crm.Logic.Models.ElementModel model)
    {
        return new ElementResponse
        {
            Id = model.Id,
            Json = model.Json,
            LastModified = model.LastModified
        };
    }
}