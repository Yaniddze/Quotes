using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Quotes.Domain.Injectable;

public static class InjectableExtension
{
    public static IServiceCollection AddInjectables(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        var types = assemblies.SelectMany(x => x.GetTypes())
            .DistinctBy(x => x.FullName)
            .Where(type => type.IsAssignableTo(typeof(IInjectable)) && type is { IsClass: true, IsAbstract: false })
            .ToList();

        foreach (var type in types)
        {
            var interfaces = type.GetInterfaces();
            
            foreach (var interfaceType in interfaces)
            {
                services.AddScoped(interfaceType, type);
            }

            services.AddScoped(type);
        }

        return services;
    }
}