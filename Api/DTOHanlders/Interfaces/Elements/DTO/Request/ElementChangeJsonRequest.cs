namespace Crm.Api.Controllers.Elements.DTO.Request;

public record ElementChangeJsonRequest
{
    public Guid Id { get; set; }
    public string? Json { get; set; }
}
