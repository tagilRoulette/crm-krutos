using Crm.Api.DTOHanlders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Api.Controllers.Pages;

[ApiController]
[Route("api/pages")]
public class PagesController : Controller
{
    private readonly IPagesDTOHandler _handler;

    public PagesController(IPagesDTOHandler handler)
    {
        _handler = handler;
    }


}
