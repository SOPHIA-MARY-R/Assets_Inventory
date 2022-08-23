using Fluid.Core.Features;
using Fluid.Shared.Models.FilterModels;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[ApiController]
[Route("api/hardware-changelogs")]
public class HardwareChangeLogController : ControllerBase
{
    private readonly IHardwareChangeLogService _hardwareChangeLogService;

    public HardwareChangeLogController(IHardwareChangeLogService hardwareChangeLogService)
    {
        _hardwareChangeLogService = hardwareChangeLogService;
    }

    [HttpPost("get-all")]
    public async Task<IActionResult> GetAllAsync(HardwareChangeLogFilter hardwareChangeLogFilter)
    {
        return Ok(await _hardwareChangeLogService.GetHardwareChangeLogsAsync(hardwareChangeLogFilter));
    }
}