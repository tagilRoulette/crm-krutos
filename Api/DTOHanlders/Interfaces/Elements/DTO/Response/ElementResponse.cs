namespace Crm.Api.Controllers.Elements.DTO.Response;

public record ElementResponse
{
    public Guid Id { get; set; }
    public string? Json { get; set; }
    public DateTime LastModified { get; set; }
}
