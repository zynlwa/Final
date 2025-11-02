using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppointmentSystem.Application.Common.Models.Identity;
using AppointmentSystem.Application.Services.Abstractions;
using System.Security.Claims;

namespace AppointmentSystem.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _identityService.RegisterAsync(registerDto);

        if (result.IsSuccess)
            return StatusCode(201, result); // Created status
        return BadRequest(result);
    }

 
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _identityService.LoginAsync(loginDto);

        if (result.IsSuccess)
            return Ok(result);
        return Unauthorized(result);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var result = await _identityService.LogoutAsync();

        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _identityService.GetUserAsync(userId);

        if (result.IsSuccess)
            return Ok(result);
        return NotFound(result);
    }

    
    [HttpPut("profile")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDto updateUserDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _identityService.UpdateUserAsync(userId, updateUserDto);

        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    
    [HttpDelete("profile")]
    [Authorize]
    public async Task<IActionResult> DeleteProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _identityService.DeleteUserAsync(userId);

        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }
}
