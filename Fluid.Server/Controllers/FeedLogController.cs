using Fluid.Core.Features;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[Route("api/feedlog/feed")]
[ApiController]
public class FeedLogController : ControllerBase
{
    private readonly IFeedLogService _feedLogService;

    public FeedLogController(IFeedLogService feedLogService)
    {
        _feedLogService = feedLogService;
    }
    
    [HttpPost]
    public async Task<IActionResult> FeedSystemConfig(SystemConfiguration systemConfiguration)
    {
        return Ok(await _feedLogService.SaveLog(systemConfiguration));
    }
}