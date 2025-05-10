using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quotes.MoexProvider.Moex.Http;
using Quotes.MoexProvider.Moex.Mapper;

namespace Quotes.MoexProvider;

public static class ServiceRegistration
{
    public static IServiceCollection AddMoexProvider(this IServiceCollection services, IConfiguration config)
    {
        services.AddHttpClient<MoexRequester>();

        var configs = MoexConfigConsumer.ProcessConfigs().ToList();
        configs.ForEach(x => services.AddSingleton(x));
        
        return services;
    }
}