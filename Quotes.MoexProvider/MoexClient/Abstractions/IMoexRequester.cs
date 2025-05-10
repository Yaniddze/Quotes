using Quotes.Domain.Injectable;
using Quotes.MoexProvider.MoexClient.Models;

namespace Quotes.MoexProvider.MoexClient.Abstractions;

public interface IMoexRequester: IInjectable
{
    Task<IEnumerable<T>> DoRequest<T>(MoexRequest request, CancellationToken token);
}