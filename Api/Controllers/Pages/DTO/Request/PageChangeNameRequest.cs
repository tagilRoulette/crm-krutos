namespace Crm.Api.Controllers.Pages.DTO.Request;

public record PageChangeNameRequest
{
    public Guid id { get; set; }
    public string Name { get; set; }
}
