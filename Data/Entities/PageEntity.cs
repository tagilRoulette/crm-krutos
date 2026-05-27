using Crm.Data.Entities;

namespace Data.Entities;

public class PageEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public Guid ProjectId { get; set; }
    public ProjectEntity Project { get; set; } = null!;

    public ICollection<CrmElementEntity> Elements { get; set; } = new List<CrmElementEntity>();
}