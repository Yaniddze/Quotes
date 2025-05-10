namespace Quotes.MoexProvider.Moex.Mapper.Abstractions;

public interface IMoexMapperConfig<T>
{
    void Configure(MoexMapperBuilder<T> builder);
}