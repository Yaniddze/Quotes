using System.Linq.Expressions;
using Quotes.MoexProvider.Moex.Mapper.Extensions;

namespace Quotes.MoexProvider.Moex.Mapper;

public class MoexMapperBuilder<T>
{
    public Dictionary<string, string> Build = new Dictionary<string, string>();

    public MoexMapperBuilder<T> HasField(Expression<Func<T, object>> to, string from)
    {
        var found = to.GetSimplePropertyAccessList();

        var name = found.First().First().Name;
        
        Build.Add(from, name);

        return this;
    }
}