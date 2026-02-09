namespace AppointmentSystem.Application.Services.Concretes;

public class AvailabilityService : IAvailabilityService
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public AvailabilityService(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AvailabilityDto> CreateAvailabilityAsync(CreateAvailabilityDto dto)
    {
        // 1️⃣ StartTime < EndTime
        if (dto.EndTime <= dto.StartTime)
            throw new ConflictException("End time must be after start time.");

        // 2️⃣ MedicalService yoxlanışı
        var ms = await _context.MedicalServices.FirstOrDefaultAsync(x => x.Id == dto.MedicalServiceId);
        if (ms == null)
            throw new NotFoundException("Medical service not found.");

        if (ms.DoctorId != dto.DoctorId)
            throw new ConflictException("Medical service does not belong to the specified doctor.");

        // 3️⃣ Work schedule yoxlanışı
        var workSchedule = await _context.DoctorWorkSchedules
            .FirstOrDefaultAsync(ws => ws.DoctorId == dto.DoctorId && ws.DayOfWeek == dto.StartTime.DayOfWeek);

        if (workSchedule == null)
            throw new ConflictException("Doctor has no work schedule for this day.");

        if (dto.StartTime < workSchedule.StartTime || dto.EndTime > workSchedule.EndTime)
            throw new ConflictException("Availability must be within doctor's work schedule.");

        // 4️⃣ Overlapping yoxlanışı
        bool overlap = await _context.Availabilities.AnyAsync(a =>
            a.DoctorId == dto.DoctorId &&
            a.StartTime < dto.EndTime &&
            dto.StartTime < a.EndTime
        );

        if (overlap)
            throw new ConflictException("This time range overlaps with an existing availability.");

        // 5️⃣ Availability yarat
        var availability = new Availability(
            dto.DoctorId,
            dto.MedicalServiceId,
            dto.StartTime,
            dto.EndTime
        );

        _context.Availabilities.Add(availability);
        await _context.SaveChangesAsync();

        return _mapper.Map<AvailabilityDto>(availability);
    }

    public async Task<IEnumerable<AvailabilityDto>> GetAvailableSlotsAsync(string doctorId, DateTime date)
    {
        var slots = await _context.Availabilities
            .Where(a => a.DoctorId == doctorId && a.StartTime.Date == date.Date && !a.IsBooked)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AvailabilityDto>>(slots);
    }

    public async Task<AvailabilityDto?> GetAvailabilityByIdAsync(string availabilityId)
    {
        var availability = await _context.Availabilities.FirstOrDefaultAsync(a => a.Id == availabilityId);
        if (availability == null) return null;

        return _mapper.Map<AvailabilityDto>(availability);
    }

    public async Task CancelAvailabilityAsync(string availabilityId, string doctorId)
    {
        var availability = await _context.Availabilities.FirstOrDefaultAsync(a => a.Id == availabilityId);
        if (availability == null)
            throw new NotFoundException("Availability slot not found.");

        if (availability.DoctorId != doctorId)
            throw new UnauthorizedAccessException("You can only cancel your own availability.");

        if (!availability.IsBooked)
            throw new ConflictException("Availability slot is not booked.");

        availability.Cancel();
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AvailabilityDto>> GetAvailabilityForDoctorAsync(string doctorId, DateTime? date = null)
    {
        var query = _context.Availabilities.AsQueryable()
            .Where(a => a.DoctorId == doctorId);

        if (date.HasValue)
            query = query.Where(a => a.StartTime.Date == date.Value.Date);

        var availabilities = await query.ToListAsync();
        return _mapper.Map<IEnumerable<AvailabilityDto>>(availabilities);
    }
}
