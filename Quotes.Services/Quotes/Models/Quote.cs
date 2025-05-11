namespace Quotes.Services.Quotes.Models;

public class Quote
{
    public DateTime Date { get; set; }
    public string Ticker { get; set; }
    public string ShortName { get; set; }
    public decimal ClosePrice { get; set; }
}