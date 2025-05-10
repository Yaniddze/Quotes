using Quotes.Domain.Injectable;
using Quotes.Domain.Quotes.Models;

namespace Quotes.Domain.Quotes.Abstractions;

public interface IQuotesProvider: IInjectable
{
    Task<Quote?> GetQuote(string ticker, DateTime date, CancellationToken token);
}