namespace AppointmentSystem.Domain.ValueObjects;

public class TimeRange
{
    public TimeRange(DateTime start, DateTime end)
    {
        if (end <= start)
            throw new ArgumentException("End must be after start");
        Start = start;
        End = end;
    }

    public DateTime Start { get; }
    public DateTime End { get; }

    public bool Overlaps(TimeRange other)
    {
        return Start < other.End && End > other.Start;
    }
}
