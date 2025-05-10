using Quotes.MoexProvider.Moex.Mapper.Models;
using Quotes.MoexProvider.Moex.Parser.Abstractions;

namespace Quotes.MoexProvider.Moex.Parser;

public class MoexParser(
    IEnumerable<MoexMapperConfig> configs
) : IMoexParser
{ 
    public async Task<IEnumerable<TResult>> Parse<TResult, TRequest>(
        TRequest request, 
        Func<TRequest, IEnumerable<string>> allColumnsSelector, 
        Func<TRequest, IEnumerable<IEnumerable<object>>> dataSelector, 
        CancellationToken token)
    {
        var resultType = typeof(TResult).Name;
        
        var foundConfig = configs
            .FirstOrDefault(x => x.ResultTypeName == resultType);

        if (foundConfig is null) throw new Exception($"не найден маппер для {resultType}");
        
        var allColumns = allColumnsSelector(request).ToArray();
        var data = dataSelector(request);

        var mapped = data
            .Select(x =>
            {
                var localResult = Activator.CreateInstance<TResult>();

                var localData = x.ToArray();

                for (var i = 0; i < allColumns.Length; i++)
                {
                    var columnItem = allColumns[i];

                    if (!foundConfig.Mappings.TryGetValue(columnItem, out var found)) continue;
                    
                    var dataItem = localData[i]?.ToString();
                    if (dataItem is null) continue;
                    
                    var resultProp = typeof(TResult).GetProperty(found)!;

                    var parsedValue = localData[i];

                    if (resultProp.PropertyType == typeof(DateTime))
                    {
                        parsedValue = DateTime.Parse(dataItem);
                    }
                    if (resultProp.PropertyType == typeof(decimal))
                    {
                        parsedValue = decimal.Parse(dataItem);
                    }

                    resultProp.SetValue(localResult, parsedValue);
                }

                return localResult;
            })
            .ToList();

        return mapped;
    }
}