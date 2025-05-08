using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Quotes.Db;

public static class ServiceRegistration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Quotes")!;

        services.AddDbContext<QuotesContext>((_, builder) =>
        {
            builder.UseNpgsql(connection);
        });
        
        return services;
    }
}