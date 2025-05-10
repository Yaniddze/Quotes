using Quotes.Domain.Quotes.Abstractions;
using Quotes.Domain.Quotes.Models;
using Quotes.MoexProvider.Moex.Client.Abstractions;
using Quotes.MoexProvider.Moex.Http.Models;
using Quotes.MoexProvider.Quotes.Models;

namespace Quotes.MoexProvider.Quotes;

public class QuotesProvider(
    IMoexClient client
) : IQuotesProvider
{
    public async Task<Quote?> GetQuote(string ticker, DateTime date, CancellationToken token)
    {
        var request = new MoexRequest(
            $"history/engines/stock/markets/shares/boards/TQBR/securities/{ticker}", 
            new Dictionary<string, string>()
            {
                {"from", date.ToString("yyyy-MM-dd")}
            });
        
        var result = await client.QueryAll<QuotesMoexRequest, Quote>(
            request,
            x => x.History.Columns,
            x => x.History.Data,
            token
        );

        return result.FirstOrDefault();
    }
}