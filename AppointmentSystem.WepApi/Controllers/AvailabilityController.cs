using AppointmentSystem.Application.Common.Models.Availability;
using AppointmentSystem.Application.Common.Models.Response;
using AppointmentSystem.Application.Services.Abstractions;
using AppointmentSystem.Application.Services.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController(IAvailabilityService availabilityService): ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAvailability([FromBody] CreateAvailabilityDto dto)
        {
            var availability = await availabilityService.CreateAvailabilityAsync(dto);
            var response = Response<AvailabilityDto>.Success(availability, 201);
            return CreatedAtAction(nameof(GetAvailabilityById), new { id = availability.Id }, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAvailabilityById(string id)
        {
            var availability = await availabilityService.GetAvailabilityByIdAsync(id);
            if (availability == null)
                return NotFound(Response<string>.Fail("Availability not found", 404));

            return Ok(Response<AvailabilityDto>.Success(availability, 200));
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetAvailableSlots(string doctorId, [FromQuery] DateTime date)
        {
            var slots = await availabilityService.GetAvailableSlotsAsync(doctorId, date);
            return Ok(Response<IEnumerable<AvailabilityDto>>.Success(slots, 200));
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelAvailability(string id)
        {
            await availabilityService.CancelAvailabilityAsync(id);
            return Ok(Response<string>.Success("Availability cancelled successfully.", 200));
        }
    }
}
