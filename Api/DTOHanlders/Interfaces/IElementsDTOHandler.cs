using Crm.Api.Controllers.Elements.DTO.Response;

namespace Crm.Api.DTOHanlders.Interfaces;

public interface IElementsDTOHandler
{
    public Task<ElementResponse> GetElementByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<ElementListResponse> GetElementsByProjectIdAsync(Guid projectId, CancellationToken cancellationToken);
    public Task<ElementListResponse> GetAllElementsAsync(CancellationToken cancellationToken);
    public Task<ElementResponse> CreateElementAsync(Guid projectId, string? json, CancellationToken cancellationToken);
    public Task ChangeElementAsync(Guid id, string json, CancellationToken cancellationToken);
    public Task<bool> DeleteElementAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteAllElementsAsync(CancellationToken cancellationToken);
}
