using Fluid.Core.Features.Masters;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[Route("api/masters/monitors")]
[ApiController]
public class MonitorMasterController : ControllerBase
{
    private readonly IMonitorMasterService _monitorMasterService;

    public MonitorMasterController(IMonitorMasterService monitorMasterService)
    {
        _monitorMasterService = monitorMasterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMonitors(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        return Ok(await _monitorMasterService.GetAllAsync(pageNumber, pageSize, searchString, orderBy));
    }

    [HttpGet("{oemSerialNo}")]
    public async Task<IActionResult> GetByIdAsync(string oemSerialNo)
    {
        return Ok(await _monitorMasterService.GetByIdAsync(oemSerialNo));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(MonitorInfo model)
    {
        return Ok(await _monitorMasterService.AddAsync(model));
    }

    [HttpPut("{oemSerialNo}")]
    public async Task<IActionResult> EditAsync(MonitorInfo model)
    {
        return Ok(await _monitorMasterService.EditAsync(model));
    }

    [HttpDelete("{oemSerialNo}")]
    public async Task<IActionResult> DeleteAsync(string oemSerialNo)
    {
        return Ok(await _monitorMasterService.DeleteAsync(oemSerialNo));
    }
}
