using System.Reflection;
using Crm.Logic.Layout;
using Microsoft.Extensions.FileProviders;

namespace Crm.Logic;

public static class LogicStartUp
{
    public static void AddLogic(this IServiceCollection services)
    {
        services.AddSingleton<LayoutStateManager>();
        services.AddSingleton<EmbeddedFileProvider>(new EmbeddedFileProvider(Assembly.GetExecutingAssembly()));
    }
}
