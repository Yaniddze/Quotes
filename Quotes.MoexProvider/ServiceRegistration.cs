using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quotes.MoexProvider.MoexClient;

namespace Quotes.MoexProvider;

public static class ServiceRegistration
{
    public static IServiceCollection AddMoexProvider(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient<MoexRequester>();
        
        return services;
    }
}