using AppointmentSystem.Application.Common.Interfaces;
using AppointmentSystem.Domain.Entities;
using System.Numerics;

namespace AppointmentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;
    private readonly IIdentityService _identityService;
    private readonly IEmailService _emailService;

    public DoctorController(IDoctorService doctorService, IIdentityService identityService, IEmailService emailService)
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
            request.ExperienceYears
        );

        var doctor = await _doctorService.CreateDoctorAsync(dto);

        // Temporary password yarat
        var tempPassword = Guid.NewGuid().ToString().Substring(0, 8) + "1!";

        // Identity user yarat
        await _identityService.RegisterDoctorAsync(new DoctorRegisterDto
        {
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Email = doctor.Email,
            UserName = doctor.Email.Split('@')[0],
            Password = tempPassword,
            ConfirmPassword = tempPassword,
            PhoneNumber = doctor.PhoneNumber,
            ExperienceYears = doctor.ExperienceYears,
            Role = "Doctor"
        });

        return CreatedAtAction(
            nameof(GetDoctorById),
            new { id = doctor.Id },
            Response<DoctorDto>.Success(doctor, 201)
        );
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Doctor")]
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
            request.PhoneNumber,
            request.ExperienceYears
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
