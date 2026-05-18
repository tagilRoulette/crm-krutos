using Crm.Api.Controllers.DTO.Request;
using Crm.Api.Controllers.DTO.Response;

namespace Crm.Api.DTOHanlders.Interfaces;

public interface IProjectDTOHandler
{
    public Task<ProjectResponse> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<ProjectListResponse> GetAllProjectsAsync(CancellationToken cancellationToken);
    public Task<ProjectResponse> CreateProjectAsync(string projectName, CancellationToken cancellationToken);

    /// <summary>
    /// Changes project's <i>JSON layout</i> and <i>navigation type</i>.
    /// </summary>
    public Task<ProjectResponse> EditProjectAsync(Guid id, ProjectEditLayoutRequest request, CancellationToken cancellationToken);
    public Task ChangeProjectNameAsync(Guid id, string newName, CancellationToken cancellationToken);
    public Task<bool> DeleteProjectAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteAllProjectsAsync(CancellationToken cancellationToken);
}
