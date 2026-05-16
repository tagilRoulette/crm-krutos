namespace Crm.Api.Controllers.DTO.Response;

public struct ProjectListResponse
{
    public IReadOnlyCollection<ProjectResponse> Projects { get; set; }

    public ProjectListResponse(IReadOnlyCollection<ProjectResponse> projects)
    {
        Projects = projects;
    }
}
