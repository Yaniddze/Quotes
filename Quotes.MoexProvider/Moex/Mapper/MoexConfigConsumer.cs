using Quotes.MoexProvider.Moex.Mapper.Abstractions;
using Quotes.MoexProvider.Moex.Mapper.Models;

namespace Quotes.MoexProvider.Moex.Mapper;

public static class MoexConfigConsumer
{
    public static IEnumerable<MoexMapperConfig> ProcessConfigs()
    {
        var found = typeof(MoexConfigConsumer).Assembly.GetExportedTypes()
            .Where(x => x.GetInterfaces().Any(z => z.IsGenericType && z.GetGenericTypeDefinition() == typeof(IMoexMapperConfig<>)) && x is { IsClass: true, IsAbstract: false })
            .ToList();

        var result = found
            .Select(x =>
            {
                var entityType = x.GetInterfaces()
                    .First(z => z.IsGenericType && z.GetGenericTypeDefinition() == typeof(IMoexMapperConfig<>))
                    .GetGenericArguments().First();
                
                var configInstance = Activator.CreateInstance(x);
                var builderType = typeof(MoexMapperBuilder<>).MakeGenericType(entityType);
                var builder = Activator.CreateInstance(builderType);

                var configureMethod = typeof(IMoexMapperConfig<>).MakeGenericType(entityType).GetMethod("Configure");
                configureMethod.Invoke(configInstance, [builder]);

                var buildProp = builderType.GetField("Build");
                var build = (Dictionary<string, string>)buildProp.GetValue(builder);

                return new MoexMapperConfig(entityType.Name, build);
            })
            .ToList();

        return result;
    }
}