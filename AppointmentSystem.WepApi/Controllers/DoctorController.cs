using AppointmentSystem.Application.Common.Models.Doctor;
using AppointmentSystem.Application.Common.Models.Response;
using AppointmentSystem.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController(IDoctorService doctorService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await doctorService.GetAllDoctorsAsync();
            var response = Response<IEnumerable<DoctorDto>>.Success(doctors, 200);
            return Ok(response);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(string id)
        {
            var doctor = await doctorService.GetDoctorByIdAsync(id);
            var response = Response<DoctorDto>.Success(doctor, 200);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto createDoctorDto)
        {
            var doctor = await doctorService.CreateDoctorAsync(createDoctorDto);
            var response = Response<DoctorDto>.Success(doctor, 201);
            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, response);
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(string id, [FromBody] UpdateDoctorDto updateDoctorDto)
        {
            await doctorService.UpdateDoctorAsync(id, updateDoctorDto);
            var response = Response<string>.Success("Doctor updated successfully.", 200);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteDoctor(string id, [FromQuery] string deletedBy)
        {
            await doctorService.SoftDeleteDoctorAsync(id, deletedBy);
            var response = Response<string>.Success("Doctor deleted successfully.", 200);
            return Ok(response);
        }

    }
}
