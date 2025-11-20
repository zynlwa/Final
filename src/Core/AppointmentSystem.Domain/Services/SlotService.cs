using AppointmentSystem.Domain.Helpers;
using AppointmentSystem.Domain.ValueObjects;

namespace AppointmentSystem.Domain.Services
{
    public interface ISlotService
    {
        List<TimeRange> GenerateSlots(
            List<TimeRange> workHours,
            List<TimeRange> breaks,
            List<TimeRange> unavailabilities,
            List<TimeRange> appointments,
            int serviceDurationMinutes);
    }

    public class SlotService : ISlotService
    {
        public List<TimeRange> GenerateSlots(
            List<TimeRange> workHours,
            List<TimeRange> breaks,
            List<TimeRange> unavailabilities,
            List<TimeRange> appointments,
            int serviceDurationMinutes)
        {
            var blocked = IntervalHelper.MergeAll(breaks, unavailabilities, appointments);
            var freeRanges = IntervalHelper.Subtract(workHours, blocked);
            return SplitIntoSlots(freeRanges, serviceDurationMinutes);
        }

        private List<TimeRange> SplitIntoSlots(List<TimeRange> ranges, int durationMinutes)
        {
            var slots = new List<TimeRange>();

            foreach (var range in ranges)
            {
                var start = range.Start;
                while (start.AddMinutes(durationMinutes) <= range.End)
                {
                    slots.Add(new TimeRange(start, start.AddMinutes(durationMinutes)));
                    start = start.AddMinutes(durationMinutes);
                }
            }

            return slots;
        }
    }
}
