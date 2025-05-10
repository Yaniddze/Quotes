using Quotes.Domain.Injectable;
using Quotes.Domain.Quotes.Models;
using Quotes.MoexProvider.Moex.Mapper;
using Quotes.MoexProvider.Moex.Mapper.Abstractions;

namespace Quotes.MoexProvider.Quotes;

public class QuotesMapper: IMoexMapperConfig<Quote>
{
    public void Configure(MoexMapperBuilder<Quote> builder)
    {
        builder
            .HasField(x => x.Ticker, "SECID")
            .HasField(x => x.ClosePrice, "CLOSE")
            .HasField(x => x.Date, "TRADEDATE");
    }
}