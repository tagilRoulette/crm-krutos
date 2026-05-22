using Crm.Api.Controllers.Elements.DTO.Request;
using Crm.Api.Controllers.Elements.DTO.Response;
using Crm.Api.Controllers.Projects.DTO.Response;
using Crm.Api.DTOHanlders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Api.Controllers.Elements;

[ApiController]
[Route("/api/elements")]
public class ElementsController : Controller
{
    private readonly IElementsDTOHandler _DTOhandler;

    public ElementsController(IElementsDTOHandler handler)
    {
        _DTOhandler = handler;
    }

    [HttpGet]
    public async Task<ActionResult<ElementListResponse>> GetAllElementsAsync(
        CancellationToken cancellationToken)
    {
        var elements = await _DTOhandler.GetAllElementsAsync(cancellationToken);
        return Ok(elements);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ElementResponse>> GetElementByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var element = await _DTOhandler.GetElementByIdAsync(id, cancellationToken);
        return Ok(element);
    }

    [HttpGet("/by-project-id/{projectId:guid}")]
    public async Task<ActionResult<ElementResponse>> GetElementsByProjectId(
        [FromRoute] Guid projectId,
        CancellationToken cancellationToken)
    {
        var element = await _DTOhandler.GetElementsByProjectIdAsync(projectId, cancellationToken);
        return Ok(element);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult<ProjectResponse>> CreateElementAsync(
        [FromBody] ElementCreateRequest request,
        CancellationToken cancellationToken)
    {
        var elements = await _DTOhandler.CreateElementAsync(request.Json, cancellationToken);
        return Ok(elements);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> ChangeElementAsync(
        [FromBody] ElementChangeJsonRequest request,
        [FromRoute] Guid id,
        CancellationToken cancellationToken
        )
    {
        await _DTOhandler.ChangeElementAsync(id, request.Json, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteElementAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
        )
    {
        var isDeletedSuccessfully = await _DTOhandler.DeleteElementAsync(id, cancellationToken);
        if (isDeletedSuccessfully) return NoContent();
        return NotFound();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAllElementsAsync(
        CancellationToken cancellationToken
        )
    {
        await _DTOhandler.DeleteAllElementsAsync(cancellationToken);
        return NoContent();
    }
}
