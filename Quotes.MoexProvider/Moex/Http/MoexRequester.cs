using Fiss;
using Quotes.MoexProvider.Moex.Http.Abstractions;
using Quotes.MoexProvider.Moex.Http.Models;

namespace Quotes.MoexProvider.Moex.Http;

public class MoexRequester(
    HttpClient client
) : IMoexRequester 
{
    public async IAsyncEnumerable<T> DoRequest<T>(MoexRequest request, CancellationToken token)
    {
        var issRequest = CreateRequest(request);
        
        if (!issRequest.ToString()!.Contains(".json")) throw new Exception("НЕ В JSON!");

        var cursor = await issRequest.ToCursorAsync<T>(
            new JsonHttpContentSerializer(), 
            client: client, 
            cancellationToken: token
        );
        
        await foreach (var value in cursor)
        {
            yield return value;
        }
    }

    private IIssRequest CreateRequest(MoexRequest model)
    {
        var request = new IssRequest();
        
        request.AddPaths(model.Url.Split("/"));
        request.AddQuery("lang", "ru");

        if (model.Queries is null) return request;
        
        foreach (var keyValuePair in model.Queries)
        {
            request.AddQuery(keyValuePair.Key, keyValuePair.Value);
        }

        return request;
    }
}