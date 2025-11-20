using AppointmentSystem.Application.Common.Models.DoctorSchedule;
using AppointmentSystem.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorScheduleController(IDoctorScheduleService service) : ControllerBase
{

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
}
