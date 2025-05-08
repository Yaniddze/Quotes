using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Quotes.Domain.Injectable;

namespace Quotes.Domain;

public static class ServiceRegistration
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services
            .AddInjectables(assemblies);
        
        return services;
    }
}