using Quotes.Domain.Injectable;

namespace Quotes.MoexProvider.Moex.Parser.Abstractions;

public interface IMoexParser: IInjectable
{
    Task<IEnumerable<TResult>> Parse<TResult, TRequest>(
        TRequest request, 
        Func<TRequest, IEnumerable<string>> allColumnsSelector, 
        Func<TRequest, IEnumerable<IEnumerable<object>>> dataSelector, 
        CancellationToken token
    );
}