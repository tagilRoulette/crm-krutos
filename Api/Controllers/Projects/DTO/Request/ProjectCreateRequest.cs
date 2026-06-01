using Crm.Logic;

namespace Crm.Api.Controllers.Projects.DTO.Request;

public record ProjectCreateRequest
{
    public string Name { get; set; }
    public NavigationType NavigationType { get; set; }
}
