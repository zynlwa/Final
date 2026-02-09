using AppointmentSystem.Application.Common.Models.Appointment;

namespace AppointmentSystem.WebApi.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /// <summary>
        /// Creates a new appointment
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDto dto)
        {
            var appointment = await _appointmentService.CreateAppointmentAsync(dto);

            return CreatedAtAction(
                nameof(GetAppointmentById),
                new { id = appointment.Id },
                Response<AppointmentDto>.Success(appointment, 201)
            );
        }

        /// <summary>
        /// Gets appointment details by id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAppointmentById(string id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);

            return Ok(Response<AppointmentDto>.Success(appointment, 200));
        }

        /// <summary>
        /// Approves an appointment (Doctor only)
        /// </summary>
        [HttpPut("{id}/approve")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> ApproveAppointment(string id)
        {
            var appointment = await _appointmentService.ApproveAppointmentAsync(id);

            return Ok(Response<AppointmentDto>.Success(appointment, 200));
        }

        /// <summary>
        /// Cancels an appointment
        /// </summary>
        [HttpPut("{id}/cancel")]
        [Authorize]
        public async Task<IActionResult> CancelAppointment(string id)
        {
            var appointment = await _appointmentService.CancelAppointmentAsync(id);

            return Ok(Response<AppointmentDto>.Success(appointment, 200));
        }

        /// <summary>
        /// Gets appointments for the currently logged-in doctor
        /// </summary>
        [HttpGet("doctor/me")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetAppointmentsForDoctor()
        {
            var appointments = await _appointmentService.GetAppointmentsForCurrentDoctorAsync();

            return Ok(Response<IEnumerable<AppointmentDto>>.Success(appointments, 200));
        }

        /// <summary>
        /// Gets appointments for a specific patient
        /// </summary>
        [HttpGet("patient/{patientId}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetAppointmentsForPatient(string patientId)
        {
            var appointments = await _appointmentService.GetAppointmentsForPatientAsync(patientId);

            return Ok(Response<IEnumerable<AppointmentDto>>.Success(appointments, 200));
        }
    }
}
