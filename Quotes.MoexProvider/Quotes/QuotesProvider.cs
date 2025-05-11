using Quotes.MoexProvider.Moex.Client.Abstractions;
using Quotes.MoexProvider.Moex.Http.Models;
using Quotes.MoexProvider.Quotes.Models;
using Quotes.Services.Quotes.Abstractions;
using Quotes.Services.Quotes.Models;

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
        
        var result = await client.QueryAll<QuotesMoexResponse, Quote>(
            request,
            x => x.History.Columns,
            x => x.History.Data,
            token
        );

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<string>> GetAvailableQuotes(DateTime date, CancellationToken token)
    {
        var request = new MoexRequest(
            $"history/engines/stock/markets/shares/boards/TQBR/securities", 
            new Dictionary<string, string>()
            {
                {"from", date.ToString("yyyy-MM-dd")}
            });
        
        var response = await client.QueryAll<QuotesMoexResponse, Quote>(
            request,
            x => x.History.Columns,
            x => x.History.Data,
            token
        );

        var data = response
            .Select(x => x.Ticker)
            .Distinct()
            .ToList();

        return data;
    }

    public async Task<DateTime> GetLastAvailableDate(CancellationToken token)
    {
        var request = new MoexRequest(
            $"history/engines/stock/markets/shares/boards/TQBR/securities", 
            new Dictionary<string, string>()
            {
                {"sort_column", "TRADEDATE"},
                {"sort_order", "desc"},
                {"first", "1"},
            });
        
        var response = await client.QueryAll<QuotesMoexResponse, Quote>(
            request,
            x => x.History.Columns,
            x => x.History.Data,
            token
        );

        var result = response.First().Date;

        return result;
    }
}