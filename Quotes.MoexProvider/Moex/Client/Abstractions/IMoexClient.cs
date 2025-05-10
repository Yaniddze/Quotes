using Quotes.Domain.Injectable;
using Quotes.MoexProvider.Moex.Http.Models;

namespace Quotes.MoexProvider.Moex.Client.Abstractions;

public interface IMoexClient: IInjectable
{
    Task<IEnumerable<TResult>> QueryAll<TRequest, TResult>(
        MoexRequest request, 
        Func<TRequest, IEnumerable<string>> allColumnsSelector, 
        Func<TRequest, IEnumerable<IEnumerable<object>>> dataSelector, 
        CancellationToken token
    );
}