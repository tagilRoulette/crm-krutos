namespace Crm.Api.Controllers.Projects.DTO.Request;

public record ProjectChangeJsonRequest
{
    public Guid Id { get; set; }
    public string NewName { get; set; }
}
