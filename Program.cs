using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


<<<<<<< HEAD
<<<<<<< HEAD

builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(options =>
=======
=======
>>>>>>> 3e3e20054a93f0369771e5a8ddd4c109efb1b5d2
builder.Services.AddSingleton<LayoutStateManager>();

builder.Services.AddSignalR();


builder.Services.AddDbContextFactory<AppDbContext>(options =>
<<<<<<< HEAD
>>>>>>> 3e3e200 (Drag & drop WIP.)
=======
>>>>>>> 3e3e20054a93f0369771e5a8ddd4c109efb1b5d2
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
<<<<<<< HEAD

=======
>>>>>>> 3e3e200 (Drag & drop WIP.)
=======
>>>>>>> 3e3e20054a93f0369771e5a8ddd4c109efb1b5d2
var app = builder.Build();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();

<<<<<<< HEAD
<<<<<<< HEAD
app.MapHub<CrmConstructorHub>("/constructorHub");
=======
app.MapHub<CrmConstructorHub>("/crmConstructorHub");
>>>>>>> 3e3e200 (Drag & drop WIP.)
=======
app.MapHub<CrmConstructorHub>("/crmConstructorHub");
>>>>>>> 3e3e20054a93f0369771e5a8ddd4c109efb1b5d2

app.MapFallbackToFile("index.html");

app.Run();