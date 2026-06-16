namespace Crm.Api.Controllers.Pages.DTO.Response;

public class PageListResponse
{
    public IReadOnlyCollection<PageResponse> Pages { get; set; }

    public PageListResponse(IReadOnlyCollection<PageResponse> pages)
    {
        Pages = pages;
    }
}
