namespace Crm.Api.Controllers.Pages.DTO.Request;

public record PageListCreateRequest
{
    public Guid ProjectId { get; set; }
    public List<string> Names { get; set; } = new();
}
