using Quotes.Domain.Injectable;
using Quotes.MoexProvider.Moex.Http.Models;

namespace Quotes.MoexProvider.Moex.Http.Abstractions;

public interface IMoexRequester: IInjectable
{
    IAsyncEnumerable<T> DoRequest<T>(MoexRequest request, CancellationToken token);
}