using Crm.Api.Controllers.Pages.DTO.Response;

namespace Crm.Api.DTOHanlders.Interfaces;

public interface IPagesDTOHandler
{
    public Task<PageResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<PageListResponse?> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken);
    // TODO Сделать Nullable<PageListResponse> ?
    public Task<PageListResponse> GetAllAsync(CancellationToken cancellationToken);
    public Task<PageResponse> CreateAsync(Guid projectId, string name, CancellationToken cancellationToken);
    public Task ChangeNameAsync(Guid pageId, string newName, CancellationToken cancellationToken);
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteAllAsync(CancellationToken cancellationToken);
    public Task<PageListResponse> CreatePagesAsync(Guid projectId, IReadOnlyList<string> names, CancellationToken cancellationToken);
}
