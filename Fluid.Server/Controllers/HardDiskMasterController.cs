using Fluid.Core.Features.Masters;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[Route("api/masters/harddisks")]
[ApiController]
public class HardDiskMasterController : ControllerBase
{
    private readonly IHardDiskMasterService _harddiskMasterService;

    public HardDiskMasterController(IHardDiskMasterService harddiskMasterService)
    {
        _harddiskMasterService = harddiskMasterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHardDisks(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        return Ok(await _harddiskMasterService.GetAllAsync(pageNumber, pageSize, searchString, orderBy));
    }

    [HttpGet("{oemSerialNo}")]
    public async Task<IActionResult> GetByIdAsync(string oemSerialNo)
    {
        return Ok(await _harddiskMasterService.GetByIdAsync(oemSerialNo));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(HardDiskInfo model)
    {
        return Ok(await _harddiskMasterService.AddAsync(model));
    }

    [HttpPut("{oemSerialNo}")]
    public async Task<IActionResult> EditAsync(HardDiskInfo model)
    {
        return Ok(await _harddiskMasterService.EditAsync(model));
    }

    [HttpDelete("{oemSerialNo}")]
    public async Task<IActionResult> DeleteAsync(string oemSerialNo)
    {
        return Ok(await _harddiskMasterService.DeleteAsync(oemSerialNo));
    }
}
