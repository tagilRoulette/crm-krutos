namespace Crm.Api.Controllers.DTO.Request;

public struct ProjectChangeNameRequest
{
    public Guid Id { get; set; }
    public string NewName { get; set; }
}
