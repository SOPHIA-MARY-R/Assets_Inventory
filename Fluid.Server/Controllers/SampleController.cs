using Fluid.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[ApiController]
public class SampleController : ControllerBase
{
    [HttpGet("api/sample/greet")]
    public IActionResult Greeting()
    {
        return Ok($"Pinging from client {Request.Host} is working successfully");
    }
}
