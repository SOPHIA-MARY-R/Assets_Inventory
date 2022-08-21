using System.Net;
using Fluid.Core.Features;
using Fluid.Core.Features.Masters;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[Route("api/masters/machines")]
[ApiController]
public class MachineMasterController : ControllerBase
{
    private readonly IMachineMasterService _machineMasterService;
    private readonly ISystemConfigurationService _systemConfigurationService;

    public MachineMasterController(IMachineMasterService machineMasterService, ISystemConfigurationService systemConfigurationService)
    {
        _machineMasterService = machineMasterService;
        _systemConfigurationService = systemConfigurationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHardDisks(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        return Ok(await _machineMasterService.GetAllAsync(pageNumber, pageSize, searchString, orderBy));
    }

    [HttpGet("{assetTag}")]
    public async Task<IActionResult> GetByIdAsync(string assetTag)
    {
        return Ok(await _systemConfigurationService.GetSystemConfiguration(WebUtility.UrlDecode(assetTag)));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(SystemConfiguration systemConfiguration)
    {
        return Ok(await _systemConfigurationService.AddSystemConfiguration(systemConfiguration));
    }

    [HttpPut("{assetTag}")]
    public async Task<IActionResult> EditAsync(SystemConfiguration systemConfiguration, string assetTag)
    {
        return Ok(await _systemConfigurationService.EditSystemConfiguration(systemConfiguration, WebUtility.UrlDecode(assetTag)));
    }

    [HttpDelete("{assetTag}")]
    public async Task<IActionResult> DeleteAsync(SystemConfiguration systemConfiguration, string assetTag)
    {
        return Ok(await _systemConfigurationService.DeleteSystemConfiguration(systemConfiguration,WebUtility.UrlDecode(assetTag)));
    }
}
