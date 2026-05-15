using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


<<<<<<< HEAD

builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(options =>
=======
builder.Services.AddSingleton<LayoutStateManager>();

builder.Services.AddSignalR();


builder.Services.AddDbContextFactory<AppDbContext>(options =>
>>>>>>> 3e3e200 (Drag & drop WIP.)
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

<<<<<<< HEAD

=======
>>>>>>> 3e3e200 (Drag & drop WIP.)
var app = builder.Build();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();

<<<<<<< HEAD
app.MapHub<CrmConstructorHub>("/constructorHub");
=======
app.MapHub<CrmConstructorHub>("/crmConstructorHub");
>>>>>>> 3e3e200 (Drag & drop WIP.)

app.MapFallbackToFile("index.html");

app.Run();