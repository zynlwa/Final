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

    [HttpPost("register/patient")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterPatient([FromBody] PatientRegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _identityService.RegisterPatientAsync(dto);

        if (result.IsSuccess)
            return StatusCode(201, result);
        return BadRequest(result);
    }

    [HttpPost("register/doctor")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RegisterDoctor([FromBody] DoctorRegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _identityService.RegisterDoctorAsync(dto);

        if (result.IsSuccess)
            return StatusCode(201, result);
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


    [HttpDelete("profile/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProfile(string id) 
    {
        if (string.IsNullOrEmpty(id))
            return BadRequest("User ID is required.");

        var result = await _identityService.DeleteUserAsync(id);

        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpDelete("profile")]
    [Authorize]
    public async Task<IActionResult> DeleteOwnProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _identityService.DeleteUserAsync(userId);

        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("forgot-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _identityService.ForgotPasswordAsync(dto);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("reset-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _identityService.ResetPasswordAsync(dto);
        return StatusCode(result.StatusCode, result);
    }
    [HttpPost("change-temporary-password")]
    [AllowAnonymous]
    public async Task<IActionResult> ChangeTemporaryPassword([FromBody] ChangeTemporaryPasswordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _identityService.ChangeTemporaryPasswordAsync(dto.Email, dto.TemporaryPassword, dto.NewPassword);

        if (result.IsSuccess)
            return Ok(result);
        return StatusCode(result.StatusCode, result);
    }


}
