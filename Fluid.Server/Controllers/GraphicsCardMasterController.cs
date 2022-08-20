using Fluid.Core.Features.Masters;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[Route("api/masters/graphicsCards")]
[ApiController]
public class GraphicsCardMasterController : ControllerBase
{
    private readonly IGraphicsCardMasterService _graphicsCardMasterService;

    public GraphicsCardMasterController(IGraphicsCardMasterService graphicsCardMasterService)
    {
        _graphicsCardMasterService = graphicsCardMasterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGraphicsCards(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        return Ok(await _graphicsCardMasterService.GetAllAsync(pageNumber, pageSize, searchString, orderBy));
    }

    [HttpGet("{oemSerialNo}")]
    public async Task<IActionResult> GetByIdAsync(string oemSerialNo)
    {
        return Ok(await _graphicsCardMasterService.GetByIdAsync(oemSerialNo));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(GraphicsCardInfo model)
    {
        return Ok(await _graphicsCardMasterService.AddAsync(model));
    }

    [HttpPut("{oemSerialNo}")]
    public async Task<IActionResult> EditAsync(GraphicsCardInfo model)
    {
        return Ok(await _graphicsCardMasterService.EditAsync(model));
    }

    [HttpDelete("{oemSerialNo}")]
    public async Task<IActionResult> DeleteAsync(string oemSerialNo)
    {
        return Ok(await _graphicsCardMasterService.DeleteAsync(oemSerialNo));
    }
}
