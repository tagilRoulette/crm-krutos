using Crm.Api.Controllers.Projects.DTO.Response;
using Crm.Logic;

namespace Crm.Api.DTOHanlders.Interfaces;

public interface IProjectDTOHandler
{
    public Task<ProjectResponse> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<ProjectListResponse> GetAllProjectsAsync(CancellationToken cancellationToken);
    public Task<ProjectResponse> CreateProjectAsync(string projectName, NavigationType navType, CancellationToken cancellationToken);
    public Task<ProjectResponse> CreateTemplateProjectAsync(CancellationToken cancellationToken);
    public Task ChangeProjectNameAsync(Guid id, string newName, CancellationToken cancellationToken);
    public Task<bool> DeleteProjectAsync(Guid id, CancellationToken cancellationToken);
    public Task DeleteAllProjectsAsync(CancellationToken cancellationToken);
}
