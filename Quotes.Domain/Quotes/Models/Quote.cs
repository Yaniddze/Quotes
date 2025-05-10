namespace Quotes.Domain.Quotes.Models;

public class Quote
{
    public DateTime Date { get; set; }
    public string Ticker { get; set; }
    public decimal ClosePrice { get; set; }
}