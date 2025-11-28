using AppointmentSystem.Application.Common.Models.Slots;
using AppointmentSystem.Domain.ValueObjects;

namespace AppointmentSystem.Application.Services.Concretes
{
    public class SlotService : ISlotService
    {
        private readonly IAppDbContext _context;
        private readonly Domain.Services.ISlotService _domainSlotService;

        public SlotService(IAppDbContext context, Domain.Services.ISlotService domainSlotService)
        {
            _context = context;
            _domainSlotService = domainSlotService;
        }

        public async Task<IEnumerable<SlotDto>> GenerateSlotsAsync(
            string doctorId,
            DateTime date,
            string medicalServiceId,
            int intervalMinutes = 15)
        {
            // 1️⃣ İş saatlarını çəkmək
            var workSchedules = await _context.DoctorWorkSchedules
                .Where(ws => ws.DoctorId == doctorId && ws.DayOfWeek == date.DayOfWeek)
                .ToListAsync();

            var workHours = workSchedules
                .Select(ws => new TimeRange(
    date.Date + ws.StartTime.TimeOfDay,
    date.Date + ws.EndTime.TimeOfDay
)
)
                .ToList();

            if (!workHours.Any())
                return Array.Empty<SlotDto>();

            // 2️⃣ Breaks
            var breaks = await _context.DoctorBreaks
    .Where(b => b.DoctorId == doctorId &&
           (b.StartTime.Date == date.Date ||
            b.IsRecurringWeekly && b.StartTime.DayOfWeek == date.DayOfWeek))
    .AsNoTracking()
    .ToListAsync();

            var breakRanges = breaks.Select(b =>
            {
                // Əgər break həftəlikdirsə, saat hissəsini soruşduğumuz günün tarixinə tətbiq et
                DateTime start = b.IsRecurringWeekly
                    ? date.Date + b.StartTime.TimeOfDay
                    : b.StartTime;

                DateTime end = b.IsRecurringWeekly
                    ? date.Date + b.EndTime.TimeOfDay
                    : b.EndTime;

                // Edge case: eğer end <= start, demək break gecəni keçir (məs: 23:30 -> 00:30)
                if (end <= start)
                {
                    // end növbəti günə keçsin
                    end = end.AddDays(1);
                }

                return new TimeRange(start, end);
            }).ToList();


            // 3️⃣ Unavailabilities
            var unavailabilities = await _context.DoctorUnavailabilities
                .Where(u => u.DoctorId == doctorId && u.StartTime.Date == date.Date)
                .ToListAsync();

            var unavailabilityRanges = unavailabilities
                .Select(u => new TimeRange(u.StartTime, u.EndTime))
                .ToList();

            // 4️⃣ Existing appointments
            var appointments = await _context.Appointments
                .Include(a => a.Availability) // ✅ Include lazımdır
                .Where(a => a.DoctorId == doctorId &&
                            a.Status != Domain.Enums.AppointmentStatus.Cancelled &&
                            a.Availability.StartTime.Date == date.Date)
                .ToListAsync();

            var appointmentRanges = appointments
                .Select(a => new TimeRange(a.Availability.StartTime, a.Availability.EndTime))
                .ToList();

            // 5️⃣ Service duration
            var service = await _context.MedicalServices
                .FirstOrDefaultAsync(s => s.Id == medicalServiceId);

            if (service == null)
                throw new InvalidOperationException("Medical service not found.");

            int durationMinutes = service.DurationMinutes;

            // 6️⃣ Domain SlotService istifadə edərək slotları generasiya et
            var slotRanges = _domainSlotService.GenerateSlots(
                workHours,
                breakRanges,
                unavailabilityRanges,
                appointmentRanges,
                durationMinutes
            );

            // 7️⃣ DTO olaraq qaytar
            return slotRanges.Select(r => new SlotDto(r.Start, r.End, r.IsBooked));

        }
    }
}
