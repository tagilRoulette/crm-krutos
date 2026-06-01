using Crm.Api;
using Crm.Data;
using Crm.Infrastructure.Hubs;
using Crm.Logic;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddControllers();
builder.Services.AddDal(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddLogic();
builder.Services.AddApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

//using (var scope = app.Services.CreateScope())
//{
//    var elementsDbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ElementsDbContext>>();
//    using (var elementsContext = elementsDbFactory.CreateDbContext())
//    {
//        elementsContext.Database.Migrate();
//    }
//    var projectsDbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ProjectsDbContext>>();
//    using (var projectsContext = projectsDbFactory.CreateDbContext())
//    {
//        projectsContext.Database.Migrate();
//    }
//    var pagesDbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<PagesDbContext>>();
//    using (var pagesContext = projectsDbFactory.CreateDbContext())
//    {
//        pagesContext.Database.Migrate();
//    }
//}
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();

app.MapHub<CrmConstructorHub>("/crmConstructorHub");

app.MapFallbackToFile("index.html");

app.Run();