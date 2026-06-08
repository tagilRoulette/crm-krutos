namespace Crm.Api.Controllers.Pages.DTO.Request;

public record PageCreateRequest
{
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = null!;
}
