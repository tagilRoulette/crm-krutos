namespace Crm.Data.Entities;

public class CrmElementEntity
{
    public Guid Id { get; set; }
    public string? Json { get; set; }
    public DateTime LastModified { get; set; }

    public Guid ProjectId { get; set; }
}