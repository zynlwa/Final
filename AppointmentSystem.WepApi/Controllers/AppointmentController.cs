using AppointmentSystem.Application.Common.Models.Appointment;
namespace AppointmentSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController (IAppointmentService appointmentService): ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto dto)
        {
            var appointment = await appointmentService.CreateAppointmentAsync(dto);
            var response = Response<AppointmentDto>.Success(appointment, 201);
            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.Id }, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(string id)
        {
            var appointment = await appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound(Response<string>.Fail("Appointment not found", 404));

            return Ok(Response<AppointmentDto>.Success(appointment, 200));
        }
        [Authorize(Roles = "Doctor")]
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveAppointment(string id)
        {
            var appointment = await appointmentService.ApproveAppointmentAsync(id);
            return Ok(Response<AppointmentDto>.Success(appointment, 200));
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelAppointment(string id)
        {
            var appointment = await appointmentService.CancelAppointmentAsync(id);
            return Ok(Response<AppointmentDto>.Success(appointment, 200));
        }

        [HttpGet("doctor-by-user/{userId}")]
        public async Task<IActionResult> GetAppointmentsForDoctorByUserId(string userId)
        {
            var appointments = await appointmentService.GetAppointmentsForDoctorByUserIdAsync(userId);
            return Ok(appointments);
        }


        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetAppointmentsForPatient(string patientId)
        {
            var appointments = await appointmentService.GetAppointmentsForPatientAsync(patientId);
            return Ok(Response<IEnumerable<AppointmentDto>>.Success(appointments, 200));
        }

    }
}
