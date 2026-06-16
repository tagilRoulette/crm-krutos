namespace Crm.Api.Controllers.Pages.DTO.Response;

public record PageResponse
{
    public PageResponse(Guid id, string name, DateTime createdAt, Guid projectId)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        ProjectId = projectId;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public Guid ProjectId { get; set; }
}
