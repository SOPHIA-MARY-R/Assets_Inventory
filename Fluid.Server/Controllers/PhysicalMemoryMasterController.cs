using Fluid.Core.Features.Masters;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[Route("api/masters/physicalmemorys")]
[ApiController]
public class PhysicalMemoryMasterController : ControllerBase
{
    private readonly IPhysicalMemoryMasterService _physicalmemoryMasterService;

    public PhysicalMemoryMasterController(IPhysicalMemoryMasterService physicalmemoryMasterService)
    {
        _physicalmemoryMasterService = physicalmemoryMasterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPhysicalMemorys(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        return Ok(await _physicalmemoryMasterService.GetAllAsync(pageNumber, pageSize, searchString, orderBy));
    }

    [HttpGet("{oemSerialNo}")]
    public async Task<IActionResult> GetByIdAsync(string oemSerialNo)
    {
        return Ok(await _physicalmemoryMasterService.GetByIdAsync(oemSerialNo));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(PhysicalMemoryModel model)
    {
        return Ok(await _physicalmemoryMasterService.AddAsync(model));
    }

    [HttpPut("{oemSerialNo}")]
    public async Task<IActionResult> EditAsync(PhysicalMemoryModel model)
    {
        return Ok(await _physicalmemoryMasterService.EditAsync(model));
    }

    [HttpDelete("{oemSerialNo}")]
    public async Task<IActionResult> DeleteAsync(string oemSerialNo)
    {
        return Ok(await _physicalmemoryMasterService.DeleteAsync(oemSerialNo));
    }
}
