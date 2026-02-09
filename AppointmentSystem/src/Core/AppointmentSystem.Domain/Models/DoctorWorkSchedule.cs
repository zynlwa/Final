namespace AppointmentSystem.Domain.Entities;

public class DoctorWorkSchedule : BaseEntity
{
    private DoctorWorkSchedule() { } // EF Core üçün

    public DoctorWorkSchedule(string doctorId, DayOfWeek dayOfWeek, DateTime  startTime, DateTime  endTime)
    {
        DoctorId = doctorId;
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
    }

    public string DoctorId { get; private set; }
    public Doctor Doctor { get; private set; } = null!; // Navigation property
   
    public DayOfWeek DayOfWeek { get; private set; }
    public DateTime  StartTime { get; private set; }
    public DateTime  EndTime { get; private set; }

    public void Update(DateTime  startTime, DateTime  endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }
}

public class DoctorBreak : BaseEntity
{
    private DoctorBreak() { }

    public DoctorBreak(string doctorId, DateTime startTime, DateTime endTime, bool isRecurringWeekly)
    {
        DoctorId = doctorId;
        StartTime = startTime;
        EndTime = endTime;
        IsRecurringWeekly = isRecurringWeekly;
    }

    public string DoctorId { get; private set; }
    public Doctor Doctor { get; private set; } = null!;

    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public bool IsRecurringWeekly { get; private set; }

    public void Update(DateTime startTime, DateTime endTime, bool isRecurringWeekly)
    {
        StartTime = startTime;
        EndTime = endTime;
        IsRecurringWeekly = isRecurringWeekly;
    }
}

public class DoctorUnavailability : BaseEntity
{
    private DoctorUnavailability() { }

    public DoctorUnavailability(string doctorId, DateTime startTime, DateTime endTime)
    {
        DoctorId = doctorId;
        StartTime = startTime;
        EndTime = endTime;
    }

    public string DoctorId { get; private set; }
    public Doctor Doctor { get; private set; } = null!;

    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    public void Update(DateTime startTime, DateTime endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }
}
