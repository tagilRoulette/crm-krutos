using Crm.Data.Contexts;
using Crm.Data.Repositories;
using Crm.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Crm.Data;

public static class DalStartUp
{
    public static void AddDal(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContextFactory<ElementsDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddDbContextFactory<ProjectsDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddDbContextFactory<PagesDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IElementsRepository, ElementsRepository>();
        services.AddScoped<IProjectsRepository, ProjectsRepository>();
    }
}
