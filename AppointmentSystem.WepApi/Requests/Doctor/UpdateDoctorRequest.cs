
namespace AppointmentSystem.WebApi.Requests.Doctor;

public class UpdateDoctorRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Specialty { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [FileLength(5)]
    [FileType("image/jpeg", "image/png")]
    public IFormFile? File { get; set; } 

    public string? PhoneNumber { get; set; }
}
