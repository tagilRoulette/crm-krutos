using Crm.Api.Controllers.Projects.DTO.Response;
using Crm.Api.DTOHanlders.Interfaces;
using Crm.Logic;
using Crm.Logic.Services.Interfaces;

namespace Crm.Api.DTOHanlders;

public class ProjectDTOHandler : IProjectDTOHandler
{
    private readonly IProjectsService _projectsService;

    public ProjectDTOHandler(IProjectsService projectsService)
    {
        _projectsService = projectsService;
    }

    public async Task ChangeProjectNameAsync(
        Guid id,
        string newName,
        CancellationToken cancellationToken)
    {
        await _projectsService.ChangeProjectNameAsync(id, newName, cancellationToken);
    }

    public async Task<ProjectResponse> CreateProjectAsync(
        string projectName,
        NavigationType navType,
        CancellationToken cancellationToken)
    {
        var project = await _projectsService.CreateProjectAsync(projectName, navType, cancellationToken);
        return new(
            project.Id,
            projectName,
            project.NavigationType,
            project.CreatedAt);
    }

    // TODO
    public Task<ProjectResponse> CreateTemplateProjectAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAllProjectsAsync(CancellationToken cancellationToken)
    {
        await _projectsService.DeleteAllProjectsAsync(cancellationToken);
    }

    public async Task<bool> DeleteProjectAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _projectsService.DeleteProjectAsync(id, cancellationToken);
    }

    public async Task<ProjectListResponse> GetAllProjectsAsync(CancellationToken cancellationToken)
    {
        var projects = await _projectsService.GetAllProjectsAsync(cancellationToken);
        ProjectListResponse projectsResponse = new(
            [.. projects.Select(x => new ProjectResponse(
                x.Id,
                x.Name,
                x.NavigationType,
                x.CreatedAt))
            ]);
        return projectsResponse;
    }

    public async Task<ProjectResponse> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var project = await _projectsService.GetProjectByIdAsync(id, cancellationToken);
        return new ProjectResponse(
            project.Id,
            project.Name,
            project.NavigationType,
            project.CreatedAt);
    }
}
