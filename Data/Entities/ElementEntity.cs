using Data.Entities;

namespace Crm.Data.Entities;

public class ElementEntity
{
    public Guid Id { get; set; }
    public string? Json { get; set; }
    public DateTime LastModified { get; set; }

    public Guid PageId { get; set; }
    public PageEntity Page { get; set; }
}