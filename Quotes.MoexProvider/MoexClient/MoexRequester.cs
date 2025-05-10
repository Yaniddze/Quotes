using Fiss;
using Fiss.Json;
using Quotes.MoexProvider.MoexClient.Abstractions;
using Quotes.MoexProvider.MoexClient.Models;

namespace Quotes.MoexProvider.MoexClient;

public class MoexRequester(
    HttpClient client
) : IMoexRequester 
{
    public async Task<IEnumerable<T>> DoRequest<T>(MoexRequest request, CancellationToken token)
    {
        var issRequest = CreateRequest(request);

        var cursor = await issRequest.ToCursorAsync<T>(
            new SystemTextJsonConverter(), 
            client: client, 
            cancellationToken: token
        );
        
        var items = new List<T>();
        await foreach (var item in cursor)
        {
            items.Add(item);
        }

        return items;
    }

    private IIssRequest CreateRequest(MoexRequest model)
    {
        var request = new IssRequest(new IssRequestOptions()
        {
            Format = Format.Json,
        });
        
        request.AddPaths(model.Url.Split("/"));
        request.AddQuery("lang", "en");

        return request;
    }
}