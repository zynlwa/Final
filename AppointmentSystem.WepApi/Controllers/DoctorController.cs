using AppointmentSystem.Application.Common.Models.Doctor;
using AppointmentSystem.Application.Common.Models.Identity;
using AppointmentSystem.Application.Common.Models.Response;
using AppointmentSystem.Application.Services.Abstractions;
using AppointmentSystem.Infrastructure.Helpers;
using AppointmentSystem.Infrastructure.Services;
using AppointmentSystem.WebApi.Requests.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.WebApi.Controllers;

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
    public async Task<IActionResult> CreateDoctor([FromForm] CreateDoctorRequest request)
    {
        string? imageUrl = null;

        if (request.File != null)
        {
            var fileName = FileManager.SaveFile(request.File, "images/doctors");
            imageUrl = $"/images/doctors/{fileName}";
        }

        var dto = new CreateDoctorDto(
            request.FirstName,
            request.LastName,
            request.Specialty,
            request.Email,
            imageUrl,
            request.PhoneNumber,
            request.Password
        );

        var doctor = await _doctorService.CreateDoctorAsync(dto);

        // Temporary password yarat
        var tempPassword = Guid.NewGuid().ToString().Substring(0, 8) + "1!";

        // Identity user yarat
        await _identityService.RegisterAsync(new RegisterDto
        {
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Email = doctor.Email,
            UserName = doctor.Email.Split('@')[0],
            Password = tempPassword,
            ConfirmPassword = tempPassword,
            PhoneNumber = doctor.PhoneNumber,
            Role = "Doctor"
        });

        // Email göndər
        _emailService.SendEmail(
            doctor.Email,
            "Your Doctor Account Created",
            $"Hello Dr. {doctor.FirstName} {doctor.LastName},<br>Your temporary password is: <b>{tempPassword}</b>"
        );

        return CreatedAtAction(
            nameof(GetDoctorById),
            new { id = doctor.Id },
            Response<DoctorDto>.Success(doctor, 201)
        );
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateDoctor(string id, [FromForm] UpdateDoctorRequest request)
    {
        string? imageUrl = null;

        if (request.File != null)
        {
            var fileName = FileManager.SaveFile(request.File, "images/doctors");
            imageUrl = $"/images/doctors/{fileName}";
        }

        var dto = new UpdateDoctorDto(
            request.FirstName,
            request.LastName,
            request.Specialty,
            request.Email,      
            imageUrl,
            request.PhoneNumber
        );

        await _doctorService.UpdateDoctorAsync(id, dto);

        return Ok(Response<string>.Success("Doctor updated successfully.", 200));
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
