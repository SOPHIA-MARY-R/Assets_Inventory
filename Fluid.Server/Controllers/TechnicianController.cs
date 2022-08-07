using Fluid.Core.Features;
using Fluid.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Fluid.Server.Controllers;

[ApiController]
public class TechnicianController : ControllerBase
{
    private readonly ITechnicianUserService _userService;

    public TechnicianController(ITechnicianUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("api/technician/login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        return Ok(await _userService.Login(loginRequest));
    }

    [HttpPost("api/technician/register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        return Ok(await _userService.RegisterAsync(registerRequest));
    }

    [HttpPost("api/technician/forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest forgotPasswordRequest)
    {
        return Ok(await _userService.ForgotPasswordAsync(forgotPasswordRequest, Request.Headers["origin"]));
    }

    [HttpPost("api/technician/refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
    {
        return Ok(await _userService.RefreshToken(refreshTokenRequest));
    }
}
