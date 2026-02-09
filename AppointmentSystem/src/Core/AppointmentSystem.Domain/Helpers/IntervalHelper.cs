using AppointmentSystem.Domain.ValueObjects;

namespace AppointmentSystem.Domain.Helpers;

public static class IntervalHelper
{
    // Merge overlapping intervals
    public static List<TimeRange> MergeAll(params List<TimeRange>[] groups)
    {
        return Merge(groups.SelectMany(x => x).ToList());
    }

    public static List<TimeRange> Merge(List<TimeRange> intervals)
    {
        if (!intervals.Any()) return new List<TimeRange>();
        // Sort intervals by start time
        var sorted = intervals.OrderBy(i => i.Start).ToList();

        var result = new List<TimeRange>();
        var current = sorted[0];

        for (int i = 1; i < sorted.Count; i++)
        {
            var next = sorted[i];
            if (next.Start <= current.End)
            {
                current = new TimeRange(current.Start, Max(current.End, next.End));
            }
            else
            {
                result.Add(current);
                current = next;
            }
        }

        result.Add(current);
        return result;
    }

    public static List<TimeRange> Subtract(List<TimeRange> ranges, List<TimeRange> blocked)
    {
        var result = new List<TimeRange>();

        foreach (var r in ranges)
        {
            var temp = new List<TimeRange> { r };
            foreach (var b in blocked)
            {
                temp = temp.SelectMany(t => SubtractOne(t, b)).ToList();
            }
            result.AddRange(temp);
        }

        return result;
    }

    private static List<TimeRange> SubtractOne(TimeRange range, TimeRange block)
    {
        if (block.End <= range.Start || block.Start >= range.End)
            return new List<TimeRange> { range };

        var result = new List<TimeRange>();
        if (block.Start > range.Start)
            result.Add(new TimeRange(range.Start, block.Start));
        if (block.End < range.End)
            result.Add(new TimeRange(block.End, range.End));

        return result;
    }

    private static DateTime Max(DateTime a, DateTime b) => a > b ? a : b;
}
