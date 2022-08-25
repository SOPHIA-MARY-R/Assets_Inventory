using Fluid.Core.Features.Masters;
using Fluid.Shared.Entities;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[Route("api/masters/processors")]
[ApiController]
public class ProcessorMasterController : ControllerBase
{
    private readonly IProcessorMasterService _processorMasterService;

    public ProcessorMasterController(IProcessorMasterService processorMasterService)
    {
        _processorMasterService = processorMasterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProcessors(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        return Ok(await _processorMasterService.GetAllAsync(pageNumber, pageSize, searchString, orderBy));
    }

    [HttpGet("{oemSerialNo}")]
    public async Task<IActionResult> GetByIdAsync(string oemSerialNo)
    {
        return Ok(await _processorMasterService.GetByIdAsync(oemSerialNo));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(ProcessorInfo model)
    {
        return Ok(await _processorMasterService.AddAsync(model));
    }

    [HttpPut("{processorId}")]
    public async Task<IActionResult> EditAsync(ProcessorInfo model)
    {
        return Ok(await _processorMasterService.EditAsync(model));
    }

    [HttpDelete("{oemSerialNo}")]
    public async Task<IActionResult> DeleteAsync(string oemSerialNo)
    {
        return Ok(await _processorMasterService.DeleteAsync(oemSerialNo));
    }
}
