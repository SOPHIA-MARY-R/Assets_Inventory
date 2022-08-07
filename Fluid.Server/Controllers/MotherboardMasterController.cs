using Fluid.Core.Features.Masters;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[Route("api/masters/motherboards")]
[ApiController]
public class MotherboardMasterController : ControllerBase
{
    private readonly IMotherboardMasterService _motherboardMasterService;

    public MotherboardMasterController(IMotherboardMasterService motherboardMasterService)
    {
        _motherboardMasterService = motherboardMasterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMotherboards(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        return Ok(await _motherboardMasterService.GetAllAsync(pageNumber, pageSize, searchString, orderBy));
    }

    [HttpGet("{oemSerialNo}")]
    public async Task<IActionResult> GetByIdAsync(string oemSerialNo)
    {
        return Ok(await _motherboardMasterService.GetByIdAsync(oemSerialNo));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(MotherboardModel model)
    {
        return Ok(await _motherboardMasterService.AddAsync(model));
    }

    [HttpPut("{oemSerialNo}")]
    public async Task<IActionResult> EditAsync(MotherboardModel model)
    {
        return Ok(await _motherboardMasterService.EditAsync(model));
    }

    [HttpDelete("{oemSerialNo}")]
    public async Task<IActionResult> DeleteAsync(string oemSerialNo)
    {
        return Ok(await _motherboardMasterService.DeleteAsync(oemSerialNo));
    }
}
