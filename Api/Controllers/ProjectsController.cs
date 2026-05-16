using Crm.Api.Controllers.DTO.Request;
using Crm.Api.Controllers.DTO.Response;
using Crm.Api.DTOsHanlders.Interfaces;
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
    public async Task<ActionResult<ProjectResponse>> GetProjectByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var project = await _DTOhandler.GetProjectByIdAsync(id, cancellationToken);
        return Ok(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public Task<ActionResult<ProjectResponse>> Create([FromBody] ProjectCreateRequest request)
    {

        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: ProjectsController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: ProjectsController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: ProjectsController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: ProjectsController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
