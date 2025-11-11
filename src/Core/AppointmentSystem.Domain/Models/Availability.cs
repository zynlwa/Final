namespace AppointmentSystem.Domain.Models;

public class Availability : BaseEntity
{
    private Availability() { }

    public Availability(string doctorId, DateTime startTime, DateTime endTime)
    {
        DoctorId = doctorId;
        StartTime = startTime;
        EndTime = endTime;
        IsBooked = false;
    }

    public string DoctorId { get; private set; }
    public Doctor Doctor { get; private set; } = null!;

    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    public bool IsBooked { get; private set; }

    public void Book() => IsBooked = true;
    public void Cancel() => IsBooked = false;
}
