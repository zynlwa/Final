namespace AppointmentSystem.Domain.ValueObjects;

public class TimeRange
{
    public TimeRange(DateTime start, DateTime end, bool isBooked = false)
    {
        if (end <= start)
            throw new ArgumentException("End must be after start");
        Start = start;
        End = end;
        IsBooked = isBooked;
    }

    public DateTime Start { get; }
    public DateTime End { get; }
    public bool IsBooked { get; set; } 

    public bool Overlaps(TimeRange other)
    {
        return Start < other.End && End > other.Start;
    }
}
