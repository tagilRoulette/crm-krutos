using Crm.Api.Controllers.Projects.DTO.Response;

namespace Crm.Api.DTOHanlders.Interfaces;

public interface IProjectDTOHandler
{
    public Task<ProjectResponse> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<ProjectListResponse> GetAllProjectsAsync(CancellationToken cancellationToken);
    public Task<ProjectResponse> CreateProjectAsync(string projectName, CancellationToken cancellationToken);
    public Task ChangeProjectNameAsync(Guid id, string newName, CancellationToken cancellationToken);
    public Task<bool> DeleteProjectAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteAllProjectsAsync(CancellationToken cancellationToken);
}
