namespace Quotes.MoexProvider.Moex.Mapper.Extensions;

internal sealed class DynamicEqualityComparer<T>(Func<T, T, bool> func) : IEqualityComparer<T>
    where T : class
{
    public bool Equals(T x, T y)
    {
        return func(x, y);
    }

    public int GetHashCode(T obj)
    {
        return 0; // force Equals
    }
}