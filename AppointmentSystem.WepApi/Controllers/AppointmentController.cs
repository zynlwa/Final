using AppointmentSystem.Application.Common.Models.Appointment;
using AppointmentSystem.Application.Common.Models.Response;
using AppointmentSystem.Application.Services.Abstractions;
using AppointmentSystem.Application.Services.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetAppointmentsForDoctor(string doctorId)
        {
            var appointments = await appointmentService.GetAppointmentsForDoctorAsync(doctorId);
            return Ok(Response<IEnumerable<AppointmentDto>>.Success(appointments, 200));
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetAppointmentsForPatient(string patientId)
        {
            var appointments = await appointmentService.GetAppointmentsForPatientAsync(patientId);
            return Ok(Response<IEnumerable<AppointmentDto>>.Success(appointments, 200));
        }

    }
}
