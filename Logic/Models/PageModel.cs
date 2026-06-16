using Crm.Data.Entities;

namespace Crm.Logic.Models;

public class PageModel
{
    public PageModel(Guid id, string name, DateTime createdAt, Guid projectId)
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
