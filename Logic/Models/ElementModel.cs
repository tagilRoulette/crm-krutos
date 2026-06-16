namespace Crm.Logic.Models;

public class ElementModel
{
    public ElementModel(Guid id, string? json, DateTime lastModified, Guid pageId)
    {
        Id = id;
        Json = json;
        LastModified = lastModified;
        PageId = pageId;
    }

    public Guid Id { get; set; }
    public string? Json { get; set; }
    public DateTime LastModified { get; set; }

    public Guid PageId { get; set; }
}
