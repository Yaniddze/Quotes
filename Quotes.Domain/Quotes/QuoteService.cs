using Quotes.Domain.Quotes.Abstractions;
using Quotes.Domain.Quotes.Models;

namespace Quotes.Domain.Quotes;

public class QuoteService(
    IQuotesProvider provider
) : IQuoteService
{
    public Task<Quote?> GetQuote(string ticker, DateTime date, CancellationToken token) =>
        provider.GetQuote(ticker, date, token);
}