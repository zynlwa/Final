using AppointmentSystem.Application.Common.Models.DoctorSchedule;
using System.Security.Claims;

namespace AppointmentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DoctorScheduleController(IDoctorScheduleService service) : ControllerBase
{
    [HttpGet("me/work-schedules")]
    public async Task<IActionResult> GetMyWorkSchedules()
    {
        // Current user ID-ni token-dən alırıq
        var appUserId = User.FindFirst("sub")?.Value; // və ya "id" claim-dən asılı olaraq

        if (string.IsNullOrEmpty(appUserId))
            return BadRequest(new { data = (object)null, isSuccess = false, statusCode = 400, errors = new[] { "User ID not found" } });

        var schedules = await service.GetWorkSchedulesForDoctorAsync(appUserId);
        return Ok(new { data = schedules, isSuccess = true, statusCode = 200 });
    }


    [HttpPost("work-schedules")]
    public async Task<IActionResult> CreateWorkSchedule([FromBody] CreateWorkScheduleDto dto)
    {
        var result = await service.CreateWorkScheduleAsync(dto);
        return CreatedAtAction(nameof(GetWorkScheduleById), new { id = result.Id }, result);
    }

    [HttpGet("work-schedules/{id}")]
    public async Task<IActionResult> GetWorkScheduleById(string id)
    {
        var result = await service.GetWorkScheduleByIdAsync(id);
        return Ok(result);
    }

    [HttpPut("work-schedules/{id}")]
    public async Task<IActionResult> UpdateWorkSchedule(string id, [FromBody] UpdateWorkScheduleDto dto)
    {
        var result = await service.UpdateWorkScheduleAsync(id, dto);
        return Ok(result);
    }

    [HttpPost("breaks")]
    public async Task<IActionResult> CreateBreak([FromBody] CreateDoctorBreakDto dto)
    {
        var result = await service.CreateBreakAsync(dto);
        return CreatedAtAction(nameof(GetBreakById), new { id = result.Id }, result);
    }

    [HttpGet("breaks/{id}")]
    public async Task<IActionResult> GetBreakById(string id)
    {
        var result = await service.GetBreakByIdAsync(id);
        return Ok(result);
    }

    [HttpPost("unavailabilities")]
    public async Task<IActionResult> CreateUnavailability([FromBody] CreateUnavailabilityDto dto)
    {
        var result = await service.CreateUnavailabilityAsync(dto);
        return CreatedAtAction(nameof(GetUnavailabilityById), new { id = result.Id }, result);
    }

    [HttpGet("unavailabilities/{id}")]
    public async Task<IActionResult> GetUnavailabilityById(string id)
    {
        var result = await service.GetUnavailabilityByIdAsync(id);
        return Ok(result);
    }
    [HttpGet("me/calendar")]
    public async Task<IActionResult> GetMyCalendar()
    {
        var appUserId =
            User.FindFirst("sub")?.Value ??
            User.FindFirst("id")?.Value ??
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(appUserId))
            return BadRequest(new
            {
                data = (object)null,
                isSuccess = false,
                statusCode = 400,
                errors = new[] { "User ID not found in token" }
            });

        var result = await service.GetDoctorCalendarAsync(appUserId);

        return Ok(new
        {
            data = result,
            isSuccess = true,
            statusCode = 200
        });
    }

    // DELETE break

    [HttpDelete("work-schedules/{id}")]
    public async Task<IActionResult> DeleteWorkSchedule(string id)
    {
        var currentUserId = User.FindFirst("sub")?.Value; // JWT token-dan AppUserId
        if (currentUserId == null) return Unauthorized();

        await service.DeleteWorkScheduleAsync(id, currentUserId);
        return NoContent();
    }

    [HttpDelete("breaks/{id}")]
    public async Task<IActionResult> DeleteBreak(string id)
    {
        var currentUserId = User.FindFirst("sub")?.Value;
        if (currentUserId == null) return Unauthorized();

        await service.DeleteBreakAsync(id, currentUserId);
        return NoContent();
    }

    [HttpDelete("unavailabilities/{id}")]
    public async Task<IActionResult> DeleteUnavailability(string id)
    {
        var currentUserId = User.FindFirst("sub")?.Value;
        if (currentUserId == null) return Unauthorized();

        await service.DeleteUnavailabilityAsync(id, currentUserId);
        return NoContent();
    }
    [HttpGet("doctor/{doctorId}/work-schedules")]
    [AllowAnonymous]
    public async Task<IActionResult> GetWorkSchedulesForDoctor(string doctorId)
    {
        var schedules = await service.GetWorkSchedulesForDoctorAsync(doctorId);
        return Ok(new { data = schedules, isSuccess = true, statusCode = 200 });
    }


}


