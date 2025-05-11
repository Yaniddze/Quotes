using Quotes.Services.Quotes.Abstractions;
using Quotes.Services.Quotes.Models;

namespace Quotes.Services.Quotes;

public class QuoteService(
    IQuotesProvider provider
) : IQuoteService
{
    public Task<Quote?> GetQuote(string ticker, DateTime date, CancellationToken token) =>
        provider.GetQuote(ticker, date, token);

    public Task<IEnumerable<string>> GetAvailableQuotes(DateTime date, CancellationToken token) =>
        provider.GetAvailableQuotes(date, token);

    public Task<DateTime> GetLastAvailableDate(CancellationToken token) =>
        provider.GetLastAvailableDate(token);
}