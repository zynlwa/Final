using AppointmentSystem.Application.Common.Models.Availability;
using AppointmentSystem.Application.Common.Models.Slots;

namespace AppointmentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AvailabilityController(IAvailabilityService availabilityService, ISlotService slotService) : ControllerBase
{
    // 1️⃣ Create a new availability
    [HttpPost]
    public async Task<IActionResult> CreateAvailability([FromBody] CreateAvailabilityDto dto)
    {
        var availability = await availabilityService.CreateAvailabilityAsync(dto);
        var response = Response<AvailabilityDto>.Success(availability, 201);
        return CreatedAtAction(nameof(GetAvailabilityById), new { id = availability.Id }, response);
    }

    // 2️⃣ Get availability by ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAvailabilityById(string id)
    {
        var availability = await availabilityService.GetAvailabilityByIdAsync(id);
        if (availability == null)
            return NotFound(Response<string>.Fail("Availability not found", 404));

        return Ok(Response<AvailabilityDto>.Success(availability, 200));
    }

    // 3️⃣ Get available slots for a doctor on a specific date
    [HttpGet("doctor/{doctorId}/slots")]
    public async Task<IActionResult> GetAvailableSlots(string doctorId, [FromQuery] DateTime date)
    {
        var slots = await availabilityService.GetAvailableSlotsAsync(doctorId, date);
        return Ok(Response<IEnumerable<AvailabilityDto>>.Success(slots, 200));
    }

    // 4️⃣ Cancel availability by ID
    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelAvailability(string id)
    {
        await availabilityService.CancelAvailabilityAsync(id);
        return Ok(Response<string>.Success("Availability cancelled successfully.", 200));

    }

    // 5️⃣ Get all availability for a doctor (məntiqi fərqli ola bilər)
    [HttpGet("doctor/{doctorId}/availability")]
    public async Task<IActionResult> GetAvailabilityForDoctor(string doctorId, [FromQuery] DateTime? date = null)
    {
        var slots = await availabilityService.GetAvailabilityForDoctorAsync(doctorId, date);
        return Ok(Response<IEnumerable<AvailabilityDto>>.Success(slots, 200));
    }

    // 6️⃣ Get available slots for a doctor and a specific service
    [HttpGet("doctor/{doctorId}/service-slots")]
    public async Task<IActionResult> GetAvailableSlotsForService(string doctorId, [FromQuery] DateTime date, [FromQuery] string serviceId)
    {
        var slots = await slotService.GenerateSlotsAsync(doctorId, date, serviceId);
        return Ok(Response<IEnumerable<SlotDto>>.Success(slots, 200));
    }
}
