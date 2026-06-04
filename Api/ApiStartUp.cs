using Crm.Api.DTOHanlders;
using Crm.Api.DTOHanlders.Interfaces;
using Crm.Data.Contexts;
using Crm.Data.Repositories;
using Crm.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Crm.Api;

public static class ApiStartUp
{
    public static void AddApi(this IServiceCollection services)
    {
        services.AddScoped<IProjectDTOHandler, ProjectDTOHandler>();
        services.AddScoped<IElementsDTOHandler, ElementsDTOHandler>();
        services.AddScoped<IPagesDTOHandler, PagesDTOHandler>();
    }
}
