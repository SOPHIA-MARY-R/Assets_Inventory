using Fluid.Core.Features.Masters;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[Route("api/masters/keyboards")]
[ApiController]
public class KeyboardMasterController : ControllerBase
{
    private readonly IKeyboardMasterService _keyboardMasterService;

    public KeyboardMasterController(IKeyboardMasterService keyboardMasterService)
    {
        _keyboardMasterService = keyboardMasterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllKeyboards(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        return Ok(await _keyboardMasterService.GetAllAsync(pageNumber, pageSize, searchString, orderBy));
    }

    [HttpGet("{oemSerialNo}")]
    public async Task<IActionResult> GetByIdAsync(string oemSerialNo)
    {
        return Ok(await _keyboardMasterService.GetByIdAsync(oemSerialNo));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(KeyboardModel model)
    {
        return Ok(await _keyboardMasterService.AddAsync(model));
    }

    [HttpPut("{oemSerialNo}")]
    public async Task<IActionResult> EditAsync(KeyboardModel model)
    {
        return Ok(await _keyboardMasterService.EditAsync(model));
    }

    [HttpDelete("{oemSerialNo}")]
    public async Task<IActionResult> DeleteAsync(string oemSerialNo)
    {
        return Ok(await _keyboardMasterService.DeleteAsync(oemSerialNo));
    }
}
