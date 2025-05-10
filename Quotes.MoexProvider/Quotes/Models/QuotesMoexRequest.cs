namespace Quotes.MoexProvider.Quotes.Models;

public class QuotesMoexRequest
{
    public QuotesMoexRequestHistory History { get; set; }
}

public class QuotesMoexRequestHistory
{
    public IEnumerable<string> Columns { get; set; }
    public IEnumerable<IEnumerable<object>> Data { get; set; }
}