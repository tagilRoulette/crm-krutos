using Crm.Data.Contexts;
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
    }
}
