using Fluid.Core.Features.Masters;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[Route("api/masters/cameras")]
[ApiController]
public class CameraMasterController : ControllerBase
{
    private readonly ICameraMasterService _cameraMasterService;

    public CameraMasterController(ICameraMasterService cameraMasterService)
    {
        _cameraMasterService = cameraMasterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCameras(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        return Ok(await _cameraMasterService.GetAllAsync(pageNumber, pageSize, searchString, orderBy));
    }

    [HttpGet("{oemSerialNo}")]
    public async Task<IActionResult> GetByIdAsync(string oemSerialNo)
    {
        return Ok(await _cameraMasterService.GetByIdAsync(oemSerialNo));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(CameraModel model)
    {
        return Ok(await _cameraMasterService.AddAsync(model));
    }

    [HttpPut("{oemSerialNo}")]
    public async Task<IActionResult> EditAsync(CameraModel model)
    {
        return Ok(await _cameraMasterService.EditAsync(model));
    }

    [HttpDelete("{oemSerialNo}")]
    public async Task<IActionResult> DeleteAsync(string oemSerialNo)
    {
        return Ok(await _cameraMasterService.DeleteAsync(oemSerialNo));
    }
}
