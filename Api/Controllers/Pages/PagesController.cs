using Crm.Api.Controllers.Pages.DTO.Request;
using Crm.Api.Controllers.Pages.DTO.Response;
using Crm.Api.DTOHanlders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Api.Controllers.Pages;

[ApiController]
[Route("api/pages")]
public class PagesController : Controller
{
    private readonly IPagesDTOHandler _DTOhandler;

    public PagesController(IPagesDTOHandler handler)
    {
        _DTOhandler = handler;
    }

    [HttpGet]
    public async Task<ActionResult<PageListResponse>> GetAllPagesAsync(
    CancellationToken cancellationToken)
    {
        var pages = await _DTOhandler.GetAllAsync(cancellationToken);
        return Ok(pages);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PageResponse>> GetPageByIdAsync(
    [FromRoute] Guid id,
    CancellationToken cancellationToken)
    {
        var page = await _DTOhandler.GetByIdAsync(id, cancellationToken);
        return Ok(page);
    }

    [HttpGet("/by-project-id/{projectId:guid}")]
    public async Task<ActionResult<PageListResponse>> GetPagesByProjectId(
    [FromRoute] Guid projectId,
    CancellationToken cancellationToken)
    {
        var page = await _DTOhandler.GetByProjectIdAsync(projectId, cancellationToken);
        return Ok(page);
    }

    [HttpPost]
    public async Task<ActionResult<PageResponse>> CreatePageAsync(
    [FromBody] PageCreateRequest request,
    CancellationToken cancellationToken)
    {
        var elements = await _DTOhandler.CreateAsync(request.ProjectId, request.Name, cancellationToken);
        return Ok(elements);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> RenamePageAsync(
    [FromBody] PageChangeNameRequest request,
    [FromRoute] Guid id,
    CancellationToken cancellationToken
    )
    {
        await _DTOhandler.ChangeNameAsync(id, request.Name, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePageAsync(
    [FromRoute] Guid id,
    CancellationToken cancellationToken
    )
    {
        await _DTOhandler.DeleteByIdAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAllPagesAsync(
    CancellationToken cancellationToken
    )
    {
        await _DTOhandler.DeleteAllAsync(cancellationToken);
        return NoContent();
    }
}
