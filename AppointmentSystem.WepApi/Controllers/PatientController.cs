
using AppointmentSystem.Application.Common.Models.Patient;
using AppointmentSystem.Application.Common.Models.Response;
using AppointmentSystem.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController(IPatientService patientService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await patientService.GetAllPatientsAsync();
            var response = Response<IEnumerable<PatientDto>>.Success(patients, 200);
            return Ok(response);
        }

        // GET: api/patient/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(string id)
        {
            var patient = await patientService.GetPatientByIdAsync(id);
            var response = Response<PatientDto>.Success(patient, 200);
            return Ok(response);
        }

        // POST: api/patient
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDto patientDto)
        {
            var patient = await patientService.CreatePatientAsync(patientDto);
            var response = Response<PatientDto>.Success(patient, 201);
            return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(string id, [FromBody] UpdatePatientDto patientDto)
        {
            await patientService.UpdatePatientAsync(id, patientDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(string id)
        {

            await patientService.SoftDeletePatientAsync(id, "System");
            return NoContent();
        }

    }
}
