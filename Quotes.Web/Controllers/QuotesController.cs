using Microsoft.AspNetCore.Mvc;
using Quotes.Services.Quotes.Abstractions;

namespace Quotes.Web.Controllers;

[ApiController]
[Route("/api/quotes")]
public class QuotesController(IQuoteService quotes): ControllerBase
{
    [HttpGet("get")]
    public async Task<IActionResult> GetAllAvailable(
        [FromQuery] DateTime date, 
        CancellationToken token
    )
    {
        var found = await quotes.GetAvailableQuotes(date, token);

        return Ok(found);
    }

    [HttpGet("get/{quote}")]
    public async Task<IActionResult> GetQuote(
        [FromRoute] string quote,
        [FromQuery] DateTime date, 
        CancellationToken token
    )
    {
        var found = await quotes.GetQuote(quote, date, token);

        return found is null ? NotFound() : Ok(found);
    }

    [HttpGet("last-date")]
    public async Task<IActionResult> GetLastDate(
        CancellationToken token
    )
    {
        var found = await quotes.GetLastAvailableDate(token);

        return Ok(found);
    }
}