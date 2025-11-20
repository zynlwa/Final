namespace AppointmentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SlotController(ISlotService slotService) : ControllerBase
{
    [HttpGet("doctor/{doctorId}")]
    public async Task<IActionResult> GetAvailableSlots(
        string doctorId,
        [FromQuery] DateTime date,
        [FromQuery] string serviceId)
    {
        var slots = await slotService.GenerateSlotsAsync(doctorId, date, serviceId);
        return Ok(slots);
    }
}
