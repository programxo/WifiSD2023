using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Wifi.SD.Core.Attributes;

namespace SD.Application.Extensions
{
    public static class ServiceBuilderExtension
    {
        public static void RegisterApplicationServices(this IServiceCollection services) 
        {
            services.Scan(scan => scan
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(c => c.WithAttribute<MapServiceDependencyAttribute>())
                .AsSelf()
                .WithScopedLifetime());
        }
    }
}
