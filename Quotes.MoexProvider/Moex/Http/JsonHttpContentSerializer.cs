using System.Text.Json;
using Fiss.Json;
using Newtonsoft.Json;

namespace Quotes.MoexProvider.Moex.Http;

public class JsonHttpContentSerializer: IHttpContentSerializer
{
    public async Task<T?> Deserialize<T>(HttpContent content, CancellationToken cancellationToken = new CancellationToken())
    {
        var payload = await content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        var parsed = JsonConvert.DeserializeObject<T>(payload);
        
        return parsed;
    }
}