namespace AppointmentSystem.Domain.Entities;

public class Patient : BaseEntity
{
    private Patient() { }

    public Patient(string firstName, string lastName, string email, string? phoneNumber, DateTime dateOfBirth, string appUserId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        AppUserId = appUserId;
    }
   
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public DateTime DateOfBirth { get; private set; }

    public string AppUserId { get; private set; } = null!;
    public AppUser AppUser { get; private set; } = null!;

    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();
    public ICollection<Review> Reviews { get; private set; } = new List<Review>();


    public void Update(string firstName, string lastName, string email, string? phoneNumber, DateTime dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
    }
}
