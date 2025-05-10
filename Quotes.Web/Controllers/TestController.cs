using Microsoft.AspNetCore.Mvc;
using Quotes.MoexProvider.MoexClient.Abstractions;
using Quotes.MoexProvider.MoexClient.Models;

namespace Quotes.Web.Controllers;

[ApiController]
[Route("/api/test")]
public class TestController(IMoexRequester client): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTests(CancellationToken token)
    {
        var data = await client.DoRequest<dynamic>(
            new MoexRequest("history/engines/stock/markets/shares/boards/TQBR/securities/SBER.json?date=2025-05-08"), 
            token
        );
        
        return Ok(data);
    }
}