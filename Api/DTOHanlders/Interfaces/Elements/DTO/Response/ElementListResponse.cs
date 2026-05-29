using Crm.Api.Controllers.Projects.DTO.Response;

namespace Crm.Api.Controllers.Elements.DTO.Response;

public class ElementListResponse
{
    public IReadOnlyCollection<ElementResponse> Elements { get; set; }

    public ElementListResponse(IReadOnlyCollection<ElementResponse> elements)
    {
        Elements = elements;
    }
}