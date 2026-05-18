using Crm.Logic;

namespace Crm.Api.Controllers.DTO.Request;

public record ProjectEditLayoutRequest
{
    public NavigationType NavigationType { get; set; }
    public string LayoutJson { get; set; }
}
