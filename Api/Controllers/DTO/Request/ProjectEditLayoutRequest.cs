namespace Crm.Api.Controllers.DTO.Request;

public struct ProjectEditLayoutRequest
{
    public Guid Id { get; set; }
    public string NavigationType { get; set; }
    public string LayoutJson { get; set; }
}
