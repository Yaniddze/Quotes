namespace Quotes.MoexProvider.Moex.Http.Models;

public record MoexRequest(string Url, Dictionary<string, string>? Queries = null);