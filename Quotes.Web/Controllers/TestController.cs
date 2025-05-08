using Microsoft.AspNetCore.Mvc;

namespace Quotes.Web.Controllers;

[ApiController]
[Route("/api/test")]
public class TestController: ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTests()
    {
        return Ok("test");
    }
}