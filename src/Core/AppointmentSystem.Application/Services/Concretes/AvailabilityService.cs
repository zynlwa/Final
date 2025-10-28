namespace AppointmentSystem.Application.Services.Concretes;

public class AvailabilityService(IAppDbContext context, IMapper mapper) : IAvailabilityService
{
    public async Task<AvailabilityDto> CreateAvailabilityAsync(CreateAvailabilityDto dto)
    {
        if (dto.EndTime <= dto.StartTime)
            throw new ConflictException("End time must be after start time.");

        var availability = mapper.Map<Availability>(dto);

        context.Availabilities.Add(availability);
        await context.SaveChangesAsync();

        return mapper.Map<AvailabilityDto>(availability);
    }

    public async Task<IEnumerable<AvailabilityDto>> GetAvailableSlotsAsync(string doctorId, DateTime date)
    {
        var slots = await context.Availabilities
            .Where(a => a.DoctorId == doctorId && a.StartTime.Date == date.Date && !a.IsBooked)
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
