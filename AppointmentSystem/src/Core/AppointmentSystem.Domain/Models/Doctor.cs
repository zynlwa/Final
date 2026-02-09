namespace AppointmentSystem.Domain.Entities;

public class Doctor : BaseEntity
{
    private Doctor() { }

    public Doctor(
        string firstName,
        string lastName,
        string email,
        string specialty,
        string? phoneNumber,
        string appUserId,
        int experienceYears,
        string? imageUrl = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Specialty = specialty;
        PhoneNumber = phoneNumber;
        AppUserId = appUserId;
        ExperienceYears = experienceYears;
        ImageUrl = imageUrl;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Specialty { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? ImageUrl { get; private set; }
    public int ExperienceYears { get; private set; }    

    public string AppUserId { get; private set; } = null!;
    public AppUser AppUser { get; private set; } = null!;

    // Slot-based system relationships
    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();
    public ICollection<MedicalService> MedicalServices { get; private set; } = new List<MedicalService>();
    public ICollection<DoctorWorkSchedule> WorkSchedules { get; private set; } = new List<DoctorWorkSchedule>();
    public ICollection<DoctorBreak> Breaks { get; private set; } = new List<DoctorBreak>();
    public ICollection<DoctorUnavailability> Unavailabilities { get; private set; } = new List<DoctorUnavailability>();
    public ICollection<Review> Reviews { get; private set; } = new List<Review>();


    // Update
    public void Update(string firstName, string lastName, string email, string? phoneNumber,int experienceYears, string specialty, string? imageUrl = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        ExperienceYears = experienceYears;
        Specialty = specialty;
        ImageUrl = imageUrl;
    }

    // Add helpers
    public void AddWorkSchedule(DoctorWorkSchedule schedule) => WorkSchedules.Add(schedule);
    public void AddBreak(DoctorBreak doctorBreak) => Breaks.Add(doctorBreak);
    public void AddUnavailability(DoctorUnavailability unavailability) => Unavailabilities.Add(unavailability);
}
