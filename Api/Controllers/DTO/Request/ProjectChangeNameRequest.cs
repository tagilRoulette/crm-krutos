namespace Crm.Api.Controllers.DTO.Request;

public record ProjectChangeNameRequest
{
    public Guid Id { get; set; }
    public string NewName { get; set; }
}
