using Crm.Api.Controllers.DTO.Request;
using Crm.Api.Controllers.DTO.Response;
using Crm.Api.DTOHanlders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Api.Controllers;

[ApiController]
[Route("/api/projects")]
public class ProjectsController : Controller
{
    private readonly IProjectDTOHandler _DTOhandler;

    public ProjectsController(IProjectDTOHandler handler)
    {
        _DTOhandler = handler;
    }

    [HttpGet]
    public async Task<ActionResult<ProjectListResponse>> GetAllProjectsAsync(CancellationToken cancellationToken)
    {
        var projects = await _DTOhandler.GetAllProjectsAsync(cancellationToken);
        return Ok(projects);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProjectResponse>> GetProjectByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var project = await _DTOhandler.GetProjectByIdAsync(id, cancellationToken);
        return Ok(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult<ProjectResponse>> CreateProjectAsync(
        [FromBody] ProjectCreateRequest request,
        CancellationToken cancellationToken)
    {
        var project = await _DTOhandler.CreateProjectAsync(request.Name, cancellationToken);
        return Ok(project);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProjectResponse>> EditProjectAsync(
        [FromBody] ProjectEditLayoutRequest request,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var project = await _DTOhandler.EditProjectAsync(id, request, cancellationToken);
        return Ok(project);
    }

    [HttpPut("{id:guid}/set-name")]
    public async Task<IActionResult> ChangeProjectNameAsync(
        [FromBody] ProjectChangeNameRequest request,
        [FromRoute] Guid id,
        CancellationToken cancellationToken
        )
    {
        await _DTOhandler.ChangeProjectNameAsync(id, request.NewName, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProjectAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
        )
    {
        var isDeletedSuccessfully = await _DTOhandler.DeleteProjectAsync(id, cancellationToken);
        if (isDeletedSuccessfully) return NoContent();
        return NotFound();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAllProjectsAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken
        )
    {
        await _DTOhandler.DeleteAllProjectsAsync(cancellationToken);
        return NoContent();
    }
}
