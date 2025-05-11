namespace Quotes.MoexProvider.Quotes.Models;

public class QuotesMoexResponse
{
    public QuotesMoexResponseHistory History { get; set; }
}

public class QuotesMoexResponseHistory
{
    public IEnumerable<string> Columns { get; set; }
    public IEnumerable<IEnumerable<object>> Data { get; set; }
}