namespace Crm.Logic.Models;

public class ProjectModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public NavigationType NavigationType { get; set; }
    public DateTime CreatedAt { get; set; }
    public ProjectModel(Guid id,
        string name,
        NavigationType navigationType,
        DateTime createdAt)
    {
        Id = id;
        Name = name;
        NavigationType = navigationType;
        CreatedAt = createdAt;
    }
}
