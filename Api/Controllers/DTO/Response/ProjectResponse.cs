using Crm.Logic;

namespace Crm.Api.Controllers.DTO.Response;

public record ProjectResponse
{
    required public Guid Id { get; set; }
    required public string Name { get; set; }
    required public NavigationType NavigationType { get; set; }
    required public DateTime CreatedAt { get; set; }
    required public string LayoutJson { get; set; }

    public ProjectResponse(
        Guid id,
        string name,
        NavigationType navigationType,
        DateTime createdAt,
        string layoutJson)
    {
        Id = id;
        Name = name;
        NavigationType = navigationType;
        CreatedAt = createdAt;
        LayoutJson = layoutJson;
    }
}
