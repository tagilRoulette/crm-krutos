namespace Crm.Data.Entities;

public class CrmElementEntity
{
    public Guid Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public DateTime LastModified { get; set; }
}