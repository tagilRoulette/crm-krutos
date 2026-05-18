namespace Crm.Api.Controllers.DTO.Response;

public record ProjectListResponse
{
    public IReadOnlyCollection<ProjectResponse> Projects { get; set; }

    public ProjectListResponse(IReadOnlyCollection<ProjectResponse> projects)
    {
        Projects = projects;
    }
}
