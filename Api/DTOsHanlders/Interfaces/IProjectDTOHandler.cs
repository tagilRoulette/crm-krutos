using Crm.Api.Controllers.DTO.Request;
using Crm.Api.Controllers.DTO.Response;

namespace Crm.Api.DTOsHanlders.Interfaces;

public interface IProjectDTOHandler
{
    public Task<ProjectResponse> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<ProjectListResponse> GetAllProjectsAsync(CancellationToken cancellationToken);
    public Task<ProjectResponse> CreateProjectAsync(ProjectCreateRequest request, CancellationToken cancellationToken);
    public Task<ProjectResponse> EditProjectAsync(ProjectCreateRequest request, CancellationToken cancellationToken);
}
