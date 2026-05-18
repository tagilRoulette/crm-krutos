using Crm.Logic;

namespace Crm.Data.Entities;

public record ProjectEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public NavigationType NavigationType { get; set; }
    public DateTime CreatedAt { get; set; }
    public string LayoutJson { get; set; }
}
