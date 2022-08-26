using Fluid.Core.Features;
using Fluid.Shared.Models.FilterModels;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }
    
    [HttpPost("feed-logs")]
    public async Task<IActionResult> GetFeedLogsReportAsync(FeedLogFilter filter)
    {
        return Ok(await _reportService.ExportFeedLogsAsync(filter));
    }
    
    [HttpPost("hardware-change-logs")]
    public async Task<IActionResult> GetHardwareChangeLogsReportAsync(HardwareChangeLogFilter filter)
    {
        return Ok(await _reportService.ExportHardwareChangeLogsAsync(filter));
    }
}