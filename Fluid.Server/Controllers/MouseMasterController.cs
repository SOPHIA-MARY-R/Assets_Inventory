using Fluid.Client.Pages.Tabs;
using Fluid.Core.Features.Masters;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[Route("api/masters/Mouses")]
[ApiController]
public class MouseMasterController : ControllerBase
{
    private readonly IMouseMasterService _MouseMasterService;

    public MouseMasterController(IMouseMasterService MouseMasterService)
    {
        _MouseMasterService = MouseMasterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMouses(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        return Ok(await _MouseMasterService.GetAllAsync(pageNumber, pageSize, searchString, orderBy));
    }

    [HttpGet("{oemSerialNo}")]
    public async Task<IActionResult> GetByIdAsync(string oemSerialNo)
    {
        return Ok(await _MouseMasterService.GetByIdAsync(oemSerialNo));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(MouseInfo model)
    {
        return Ok(await _MouseMasterService.AddAsync(model));
    }

    [HttpPut("{oemSerialNo}")]
    public async Task<IActionResult> EditAsync(MouseInfo model)
    {
        return Ok(await _MouseMasterService.EditAsync(model));
    }

    [HttpDelete("{oemSerialNo}")]
    public async Task<IActionResult> DeleteAsync(string oemSerialNo)
    {
        return Ok(await _MouseMasterService.DeleteAsync(oemSerialNo));
    }
}
