using Crm.Api.Controllers.Pages.DTO.Response;
using Crm.Api.DTOHanlders.Interfaces;
using Crm.Logic.Services.Interfaces;

namespace Crm.Api.DTOHanlders;

public class PagesDTOHandler : IPagesDTOHandler
{
    private readonly IPagesService _pagesService;

    public PagesDTOHandler(IPagesService pagesService)
    {
        _pagesService = pagesService;
    }

    public async Task ChangeNameAsync(Guid pageId, string newName, CancellationToken cancellationToken)
    {
        await _pagesService.ChangeNameAsync(pageId, newName, cancellationToken);
    }

    public async Task<PageResponse> CreateAsync(Guid projectId, string name, CancellationToken cancellationToken)
    {
        var model = await _pagesService.CreateAsync(projectId, name, cancellationToken);
        return new(model.Id, model.Name, model.CreatedAt, model.ProjectId);
    }

    public async Task<PageListResponse> CreatePagesAsync(Guid projectId, IReadOnlyList<string> names, CancellationToken cancellationToken)
    {
        var models = await _pagesService.CreatePagesAsync(projectId, names, cancellationToken);
        return new([.. models.Select(z => new PageResponse(z.Id, z.Name, z.CreatedAt, z.ProjectId))]);
    }

    public Task DeleteAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<PageListResponse> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<PageResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<PageListResponse?> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
