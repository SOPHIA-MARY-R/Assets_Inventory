using Fluid.Core.Features.Masters;
using Fluid.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[Route("api/masters/speakers")]
[ApiController]
public class SpeakerMasterController : ControllerBase
{
    private readonly ISpeakerMasterService _speakerMasterService;

    public SpeakerMasterController(ISpeakerMasterService speakerMasterService)
    {
        _speakerMasterService = speakerMasterService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSpeakers(int pageNumber, int pageSize, string searchString, string orderBy)
    {
        return Ok(await _speakerMasterService.GetAllAsync(pageNumber, pageSize, searchString, orderBy));
    }

    [HttpGet("{oemSerialNo}")]
    public async Task<IActionResult> GetByIdAsync(string oemSerialNo)
    {
        return Ok(await _speakerMasterService.GetByIdAsync(oemSerialNo));
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(SpeakerModel model)
    {
        return Ok(await _speakerMasterService.AddAsync(model));
    }

    [HttpPut("{oemSerialNo}")]
    public async Task<IActionResult> EditAsync(SpeakerModel model)
    {
        return Ok(await _speakerMasterService.EditAsync(model));
    }

    [HttpDelete("{oemSerialNo}")]
    public async Task<IActionResult> DeleteAsync(string oemSerialNo)
    {
        return Ok(await _speakerMasterService.DeleteAsync(oemSerialNo));
    }
}
