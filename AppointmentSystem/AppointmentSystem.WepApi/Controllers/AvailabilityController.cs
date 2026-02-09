using AppointmentSystem.Application.Common.Models.Availability;
using AppointmentSystem.Application.Common.Models.Slots;
using AppointmentSystem.Domain.Exceptions;

namespace AppointmentSystem.WebApi.Controllers;

[Route("api/availabilities")]
[ApiController]
[Authorize] // Default bütün endpoint-lər üçün login tələb olunur
public class AvailabilityController : ControllerBase
{
    private readonly IAvailabilityService _availabilityService;
    private readonly ISlotService _slotService;
    private readonly ICurrentUserService _currentUserService;

    public AvailabilityController(
        IAvailabilityService availabilityService,
        ISlotService slotService,
        ICurrentUserService currentUserService)
    {
        _availabilityService = availabilityService;
        _slotService = slotService;
        _currentUserService = currentUserService;
    }

    /// <summary>
    /// Doctor-un availability yaratması (yalnız Doctor)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> CreateAvailability([FromBody] CreateAvailabilityRequest dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(Response<string>.Fail("Invalid input data", 400));

        try
        {
            var availability = await _availabilityService.CreateAvailabilityAsync(
                new CreateAvailabilityDto(
                    DoctorId: _currentUserService.UserId,
                    MedicalServiceId: dto.MedicalServiceId,
                    StartTime: dto.StartTime,
                    EndTime: dto.EndTime
                )
            );

            return CreatedAtAction(nameof(GetAvailabilityById), new { id = availability.Id },
                Response<AvailabilityDto>.Success(availability, 201));
        }
        catch (ConflictException ex)
        {
            return Conflict(Response<string>.Fail(ex.Message, 409));
        }
        catch (NotFoundException ex)
        {
            return NotFound(Response<string>.Fail(ex.Message, 404));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Response<string>.Fail("Internal server error: " + ex.Message, 500));
        }
    }

    /// <summary>
    /// Availability-i ID ilə əldə etmək
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAvailabilityById(string id)
    {
        try
        {
            var availability = await _availabilityService.GetAvailabilityByIdAsync(id);
            if (availability == null)
                return NotFound(Response<string>.Fail("Availability not found", 404));

            return Ok(Response<AvailabilityDto>.Success(availability, 200));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Response<string>.Fail("Internal server error: " + ex.Message, 500));
        }
    }

    /// <summary>
    /// Current doctor-un availability-lərini əldə etmək (yalnız Doctor)
    /// </summary>
    [HttpGet("doctor/me")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> GetAvailabilityForCurrentDoctor([FromQuery] DateTime? date = null)
    {
        try
        {
            var doctorId = _currentUserService.UserId;
            var availabilities = await _availabilityService.GetAvailabilityForDoctorAsync(doctorId, date);

            return Ok(Response<IEnumerable<AvailabilityDto>>.Success(availabilities, 200));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Response<string>.Fail("Internal server error: " + ex.Message, 500));
        }
    }

    /// <summary>
    /// Availability-i ləğv etmək (yalnız Doctor)
    /// </summary>
    [HttpPut("{id}/cancel")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> CancelAvailability(string id)
    {
        try
        {
            var doctorId = _currentUserService.UserId;
            await _availabilityService.CancelAvailabilityAsync(id, doctorId);

            return Ok(Response<string>.Success("Availability cancelled successfully.", 200));
        }
        catch (ConflictException ex)
        {
            return Conflict(Response<string>.Fail(ex.Message, 409));
        }
        catch (NotFoundException ex)
        {
            return NotFound(Response<string>.Fail(ex.Message, 404));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Response<string>.Fail("Internal server error: " + ex.Message, 500));
        }
    }

    /// <summary>
    /// Həkim üçün müəyyən gündə mövcud slot-ları əldə etmək (Patient baxa bilər)
    /// </summary>
    [HttpGet("doctor/{doctorId}/slots")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAvailableSlots(string doctorId, [FromQuery] DateTime date)
    {
        try
        {
            // UTC-normalized date
            date = date.ToUniversalTime();

            var availabilities = await _availabilityService.GetAvailableSlotsAsync(doctorId, date);

            // Map AvailabilityDto → SlotDto
            var slots = availabilities.Select(a => new SlotDto(a.StartTime, a.EndTime, a.IsBooked));

            return Ok(Response<IEnumerable<SlotDto>>.Success(slots, 200));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Response<string>.Fail("Internal server error: " + ex.Message, 500));
        }
    }

    /// <summary>
    /// Həkim və service üzrə mövcud slot-ları əldə etmək (Patient baxa bilər)
    /// </summary>
    [HttpGet("doctor/{doctorId}/service-slots")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAvailableSlotsForService(
        string doctorId,
        [FromQuery] DateTime date,
        [FromQuery] string serviceId)
    {
        try
        {
            // UTC-normalized date
            date = date.ToUniversalTime();

            var slots = await _slotService.GenerateSlotsAsync(doctorId, date, serviceId);

            return Ok(Response<IEnumerable<SlotDto>>.Success(slots, 200));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Response<string>.Fail("Internal server error: " + ex.Message, 500));
        }
    }
}

/// <summary>
/// Validation üçün DTO (CreateAvailabilityDto-dan fərqli, init-only property-lara workaround)
/// </summary>
public class CreateAvailabilityRequest
{
    [Required]
    public string MedicalServiceId { get; set; } = null!;

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime StartTime { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime EndTime { get; set; }
}
