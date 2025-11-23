namespace AppointmentSystem.Domain.Models;

public class Review:BaseEntity
{
    private Review() { } // For EF Core
    public string PatientId { get; private set; }
    public string DoctorId { get; private set; }
    public int Rating { get; private set; } // 1-5
    public string Comment { get; private set; }

    public Patient Patient { get; private set; }
    public Doctor Doctor { get; private set; }

    // Constructor – Creation zamanı initialize edilir
    public Review(string patientId, string doctorId, int rating, string comment)
    {
        PatientId = patientId;
        DoctorId = doctorId;
        Rating = rating;
        Comment = comment;
    }

}
