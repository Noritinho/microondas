using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microwave.Application.Contracts.Requests;
using Microwave.Application.Contracts.Responses;
using Microwave.Application.UseCases;
using Microwave.Domain.Contracts.Requests;
using Microwave.Domain.Contracts.Responses;

namespace Microwave.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MicroWaveController : ControllerBase
{
    [HttpPost("set")]
    [ProducesResponseType(typeof(SetHeatingResponse), StatusCodes.Status201Created)]
    [Authorize]
    public ActionResult<SetHeatingResponse> SetHeatAsync(
        [FromServices] ISetHeatingUseCase setHeatingUseCase,
        [FromBody] SetHeatingRequest request)
    {
        var userId = int.Parse(User.FindFirst("id")?.Value);

        var heatingResponse = setHeatingUseCase.SetHeating(request, userId);
        
        return Created(string.Empty, heatingResponse);
    }
    
    [HttpPost("setHeatPreset")]
    [ProducesResponseType(typeof(SetHeatingResponse), StatusCodes.Status201Created)]
    [Authorize]
    public async Task<ActionResult<SetHeatingResponse>> SetHeatPresetAsync(
        [FromServices] ISetHeatingUseCase setHeatingUseCase,
        [FromBody] SetHeatingPresetRequest request)
    {
        var userId = int.Parse(User.FindFirst("id")?.Value);

        var heatingResponse = await setHeatingUseCase.SetHeatingPreset(request, userId);
        
        return Created(string.Empty, heatingResponse);
    }
    
    [HttpPut("cancel")]
    [ProducesResponseType(typeof(SetHeatingResponse), StatusCodes.Status200OK)]
    [Authorize]
    public void PauseOrCancelHeatAsync(
        [FromServices] ISetHeatingUseCase setHeatingUseCase)
    {
        var userId = int.Parse(User.FindFirst("id")?.Value);

        setHeatingUseCase.PauseOrCancel(userId);

        return;
    }
    
    [HttpPost("preset")]
    [ProducesResponseType(typeof(CreateHeatingPresetResponse), StatusCodes.Status201Created)]
    [Authorize]
    public async Task<ActionResult<CreateHeatingPresetResponse>> CreateHeatingPreset(
        [FromServices] IHeatingPresetUseCases useCases,
        [FromBody] CreateHeantigPresetRequest request)
    {
        var response = await useCases.CreateAsync(request);
        
        return Created(string.Empty, response);
    }

    [HttpGet("preset")]
    [ProducesResponseType(typeof(HeatingPresetResponse), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult<HeatingPresetResponse>> GetHeatingPresets(
        [FromServices] IHeatingPresetUseCases useCases)
    {
        var response = await useCases.GetPresetsAsync().ToListAsync();

        return Ok(response);
    }
}