﻿using Fluid.Core.Features;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Fluid.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[Route("api/feed-log")]
[ApiController]
public class FeedLogController : ControllerBase
{
    private readonly IFeedLogService _feedLogService;

    public FeedLogController(IFeedLogService feedLogService)
    {
        _feedLogService = feedLogService;
    }

    [HttpPost]
    public async Task<IActionResult> GetAllAsync(int pageNumber, int pageSize, FeedLogFilter feedLogFilter)
    {
        return Ok(await _feedLogService.GetAllAsync(pageNumber, pageSize, feedLogFilter));
    }
    
    [HttpPost("feed")]
    public async Task<IActionResult> FeedSystemConfig(SystemConfiguration systemConfiguration)
    {
        return Ok(await _feedLogService.SaveLog(systemConfiguration));
    }

    [HttpPost("{id}/attend")]
    public async Task<IActionResult> Attend(string id, FeedLog feedLog)
    {
        return Ok(await _feedLogService.AttendLog(feedLog));
    }
}