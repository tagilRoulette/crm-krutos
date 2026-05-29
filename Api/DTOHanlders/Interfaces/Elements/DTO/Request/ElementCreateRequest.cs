namespace Crm.Api.Controllers.Elements.DTO.Request;

public record ElementCreateRequest
{
    public Guid ProjectId { get; set; }
    public string? Json { get; set; }
}
