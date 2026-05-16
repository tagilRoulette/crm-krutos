namespace Crm.Data.Entities;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NavigationType { get; set; }
    public DateTime CreatedAt { get; set; }
    public string LayoutJson { get; set; }
}
