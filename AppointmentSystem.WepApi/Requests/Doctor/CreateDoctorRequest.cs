namespace AppointmentSystem.WebApi.Requests.Doctor;

public class CreateDoctorRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
    [Required]
    public string Specialty { get; set; }
   

    [FileLength(5)] 
    [FileType("image/jpeg", "image/png")] 
    public IFormFile File { get; set; }

    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
}


