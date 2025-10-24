namespace AppointmentSystem.Domain.Entities;

public class Doctor : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Specialty { get; private set; }
    public string? PhoneNumber { get; private set; }
   
    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();
    public ICollection<Availability> Availabilities { get; private set; } = new List<Availability>();

    public Doctor(string firstName, string lastName, string email, string specialty, string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");
        if (string.IsNullOrWhiteSpace(specialty))
            throw new ArgumentException("Specialty is required.");

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Specialty = specialty;
        PhoneNumber = phoneNumber;
    }

    public void UpdateDoctor(string firstName, string lastName, string email, string? phoneNumber, string specialty)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name is required.");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name is required.");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");
        if (string.IsNullOrWhiteSpace(specialty))
            throw new ArgumentException("Specialty is required.");

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Specialty = specialty;
    }
}
