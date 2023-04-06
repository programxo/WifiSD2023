using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Wifi.SD.Core.Attributes;

namespace SD.Persistence.Extensions
{
    public static class ServiceBuilderExtension
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(c => c.WithAttribute<MapServiceDependencyAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            );
        }
    }
}
