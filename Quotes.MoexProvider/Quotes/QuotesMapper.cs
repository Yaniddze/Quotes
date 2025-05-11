using Quotes.MoexProvider.Moex.Mapper;
using Quotes.MoexProvider.Moex.Mapper.Abstractions;
using Quotes.Services.Quotes.Models;

namespace Quotes.MoexProvider.Quotes;

public class QuotesMapper: IMoexMapperConfig<Quote>
{
    public void Configure(MoexMapperBuilder<Quote> builder)
    {
        builder
            .HasField(x => x.Ticker, "SECID")
            .HasField(x => x.ClosePrice, "CLOSE")
            .HasField(x => x.ShortName, "SHORTNAME")
            .HasField(x => x.Date, "TRADEDATE");
    }
}