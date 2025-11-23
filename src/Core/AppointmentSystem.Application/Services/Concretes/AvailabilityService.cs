namespace AppointmentSystem.Application.Services.Concretes;

public class AvailabilityService(IAppDbContext context, IMapper mapper) : IAvailabilityService
{
    public async Task<AvailabilityDto> CreateAvailabilityAsync(CreateAvailabilityDto dto)
    {
        // 1) StartTime < EndTime
        if (dto.EndTime <= dto.StartTime)
            throw new ConflictException("End time must be after start time.");

        // 2) MedicalService yoxlanışı
        var ms = await context.MedicalServices
            .FirstOrDefaultAsync(x => x.Id == dto.MedicalServiceId);

        if (ms == null)
            throw new NotFoundException("Medical service not found.");

        if (ms.DoctorId != dto.DoctorId)
            throw new ConflictException("Medical service does not belong to the specified doctor.");

        // 3) Overlapping yoxlaması
        bool overlap = await context.Availabilities.AnyAsync(a =>
            a.DoctorId == dto.DoctorId &&
            a.StartTime < dto.EndTime &&
            dto.StartTime < a.EndTime
        );

        if (overlap)
            throw new ConflictException("This time range overlaps with an existing availability.");

        // 4) Availability yarat
        var availability = new Availability(
            dto.DoctorId,
            dto.MedicalServiceId,
            dto.StartTime,
            dto.EndTime
        );

        context.Availabilities.Add(availability);
        await context.SaveChangesAsync();

        return mapper.Map<AvailabilityDto>(availability);
    }

    public async Task<IEnumerable<AvailabilityDto>> GetAvailableSlotsAsync(string doctorId, DateTime date)
    {
        var slots = await context.Availabilities
            .Where(a => a.DoctorId == doctorId &&
                        a.StartTime.Date == date.Date &&
                        !a.IsBooked)
            .ToListAsync();

        return mapper.Map<IEnumerable<AvailabilityDto>>(slots);
    }

    public async Task<AvailabilityDto?> GetAvailabilityByIdAsync(string availabilityId)
    {
        var availability = await context.Availabilities
            .FirstOrDefaultAsync(a => a.Id == availabilityId);

        if (availability == null)
            throw new NotFoundException("Availability slot not found.");

        return mapper.Map<AvailabilityDto>(availability);
    }

    public async Task CancelAvailabilityAsync(string availabilityId)
    {
        var availability = await context.Availabilities
            .FirstOrDefaultAsync(a => a.Id == availabilityId);

        if (availability == null)
            throw new NotFoundException("Availability slot not found.");

        if (!availability.IsBooked)
            throw new ConflictException("Availability slot is not booked.");

        availability.Cancel();

        await context.SaveChangesAsync();
    }
}
