using Quotes.MoexProvider.Moex.Client.Abstractions;
using Quotes.MoexProvider.Moex.Http.Abstractions;
using Quotes.MoexProvider.Moex.Http.Models;
using Quotes.MoexProvider.Moex.Parser.Abstractions;

namespace Quotes.MoexProvider.Moex.Client;

public class MoexClient(
    IMoexRequester requester,
    IMoexParser parser
) : IMoexClient
{
    public async Task<IEnumerable<TResult>> QueryAll<TRequest, TResult>(
        MoexRequest request, 
        Func<TRequest, IEnumerable<string>> allColumnsSelector, 
        Func<TRequest, IEnumerable<IEnumerable<object>>> dataSelector, 
        CancellationToken token
    )
    {
        var result = new List<TResult>();
        
        await foreach (var item in requester.DoRequest<TRequest>(request, token))
        {
            var parsed = await parser.Parse<TResult, TRequest>(
                item,
                allColumnsSelector,
                dataSelector,
                token
            );
            
            result.AddRange(parsed);
        }

        return result;
    }
}