using Crm.Api.Controllers.Projects.DTO.Response;

namespace Crm.Api.Controllers.Elements.DTO.Response;

public class ElementListResponse
{
    public IReadOnlyCollection<ProjectResponse> Projects { get; set; }

    public ElementListResponse(IReadOnlyCollection<ProjectResponse> projects)
    {
        Projects = projects;
    }
}
