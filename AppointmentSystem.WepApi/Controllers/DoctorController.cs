using AppointmentSystem.Application.Common.Models.Doctor;
using AppointmentSystem.Application.Common.Models.Identity;
using AppointmentSystem.Application.Common.Models.Response;
using AppointmentSystem.Application.Services.Abstractions;
using AppointmentSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IIdentityService _identityService;
        private readonly EmailService _emailService;

        public DoctorController(IDoctorService doctorService, IIdentityService identityService, EmailService emailService)
        {
            _doctorService = doctorService;
            _identityService = identityService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            var response = Response<IEnumerable<DoctorDto>>.Success(doctors, 200);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(string id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            var response = Response<DoctorDto>.Success(doctor, 200);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto createDoctorDto)
        {
            // 1️⃣ Doktoru yaradın
            var doctor = await _doctorService.CreateDoctorAsync(createDoctorDto);

            // 2️⃣ Temporary password yarat
            var tempPassword = Guid.NewGuid().ToString().Substring(0, 8) + "1!";

            // 3️⃣ AppUser üçün Identity yarat
            var registerDto = new RegisterDto
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email,
                UserName = doctor.Email.Split('@')[0],
                Password = tempPassword,
                ConfirmPassword = tempPassword,
                PhoneNumber = doctor.PhoneNumber,
                Role = "Doctor"
            };
            await _identityService.RegisterAsync(registerDto);

            // 4️⃣ Email göndər
            var body = $@"
        Hello Dr. {doctor.FirstName} {doctor.LastName},<br><br>
        Your account has been created.<br>
        Temporary Password: <b>{tempPassword}</b><br>
        Please login using Postman at:<br>
        POST /api/Identity/login
    ";
            _emailService.SendEmail(doctor.Email, "Your Doctor Account Created", body);

            var response = Response<DoctorDto>.Success(doctor, 201);
            return CreatedAtAction(nameof(GetDoctorById), new { id = doctor.Id }, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDoctor(string id, [FromBody] UpdateDoctorDto updateDoctorDto)
        {
            await _doctorService.UpdateDoctorAsync(id, updateDoctorDto);
            var response = Response<string>.Success("Doctor updated successfully.", 200);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SoftDeleteDoctor(string id, [FromQuery] string deletedBy)
        {
            await _doctorService.SoftDeleteDoctorAsync(id, deletedBy);
            var response = Response<string>.Success("Doctor deleted successfully.", 200);
            return Ok(response);
        }
    }
}
