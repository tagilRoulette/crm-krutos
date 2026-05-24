using Crm.Data;
using Crm.Infrastructure.Hubs;
using Crm.Logic;
using Crm.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Crm.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddDal(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddLogic();
builder.Services.AddApi();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .SetIsOriginAllowed(_ => true);
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var elementsDbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ElementsDbContext>>();
    using (var elementsContext = elementsDbFactory.CreateDbContext())
    {
        elementsContext.Database.Migrate();
    }
    var projectsDbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ProjectsDbContext>>();
    using (var projectsContext = projectsDbFactory.CreateDbContext())
    {
        projectsContext.Database.Migrate();
    }
}
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();

app.MapHub<CrmConstructorHub>("/crmConstructorHub");

app.MapFallbackToFile("index.html");

app.Run();