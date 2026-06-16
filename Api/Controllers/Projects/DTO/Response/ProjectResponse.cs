using Crm.Logic;

namespace Crm.Api.Controllers.Projects.DTO.Response;

public record ProjectResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public NavigationType NavigationType { get; set; }
    public DateTime CreatedAt { get; set; }

    public ProjectResponse(
        Guid id,
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
