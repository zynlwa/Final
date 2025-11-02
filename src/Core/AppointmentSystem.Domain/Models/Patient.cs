namespace AppointmentSystem.Domain.Entities;

public class Patient : BaseEntity
{
    private Patient() { }

    public Patient(string firstName, string lastName, string email, string? phoneNumber, DateTime dateOfBirth, string appUserId)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");
        if (dateOfBirth == default)
            throw new ArgumentException("Date of birth is required.");

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

    public string? AppUserId { get; private set; }
    public AppUser? AppUser { get; private set; } = null!;

    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();

    public void Update(string firstName, string lastName, string email, string? phoneNumber, DateTime dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");
        if (dateOfBirth == default)
            throw new ArgumentException("Date of birth is required.");

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
    }
}
