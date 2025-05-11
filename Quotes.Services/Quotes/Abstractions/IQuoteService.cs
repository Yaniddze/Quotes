using Quotes.Domain.Injectable;
using Quotes.Services.Quotes.Models;

namespace Quotes.Services.Quotes.Abstractions;

public interface IQuoteService: IInjectable
{
    Task<Quote?> GetQuote(string ticker, DateTime date, CancellationToken token);
    Task<IEnumerable<string>> GetAvailableQuotes(DateTime date, CancellationToken token);
    Task<DateTime> GetLastAvailableDate(CancellationToken token);
}