using Microsoft.AspNetCore.Mvc;
using Quotes.Domain.Quotes.Abstractions;
using Quotes.MoexProvider.Moex.Http.Abstractions;
using Quotes.MoexProvider.Moex.Http.Models;

namespace Quotes.Web.Controllers;

[ApiController]
[Route("/api/test")]
public class TestController(IQuoteService quotes): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTests(CancellationToken token)
    {
        var found = await quotes.GetQuote("SBER", new DateTime(2025, 05, 08), token);

        return found is null ? Ok("не найдено") : Ok(found);
    }
}