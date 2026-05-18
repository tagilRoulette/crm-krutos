using Crm.Api.Controllers.DTO.Request;
using Crm.Api.Controllers.DTO.Response;
using Crm.Api.DTOHanlders.Interfaces;
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
        CancellationToken cancellationToken)
    {
        var project = await _projectsService.CreateProjectAsync(projectName, cancellationToken);
        return new(project.Id, projectName, project.NavigationType, project.CreatedAt, );
    }

    public Task DeleteAllProjectsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProjectAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ProjectResponse> EditProjectAsync(
        Guid id,
        ProjectEditLayoutRequest request,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ProjectListResponse> GetAllProjectsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ProjectResponse> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
