using AppointmentSystem.Application.Common.Models.DoctorSchedule;

namespace AppointmentSystem.Application.Services.Concretes;

public class DoctorScheduleService(IAppDbContext context, IMapper mapper) : IDoctorScheduleService
{

    public async Task<IEnumerable<WorkScheduleDto>> GetWorkSchedulesForDoctorAsync(string doctorId, bool byAppUserId = false)
    {
        IQueryable<DoctorWorkSchedule> query = context.DoctorWorkSchedules;

        if (byAppUserId)
        {
            query = query.Where(s => s.Doctor.AppUserId == doctorId);
        }
        else
        {
            query = query.Where(s => s.Doctor.Id == doctorId); // burda frontend-dən gələn Doctor.Id istifadə olunur
        }

        var schedules = await query.ToListAsync();
        return mapper.Map<IEnumerable<WorkScheduleDto>>(schedules);
    }



    public async Task<WorkScheduleDto> CreateWorkScheduleAsync(CreateWorkScheduleDto dto)
    {
        var entity = new DoctorWorkSchedule(dto.DoctorId, dto.DayOfWeek, dto.StartTime, dto.EndTime);
        context.DoctorWorkSchedules.Add(entity);
        await context.SaveChangesAsync();
        return mapper.Map<WorkScheduleDto>(entity);
    }

    public async Task<WorkScheduleDto> GetWorkScheduleByIdAsync(string id)
    {
        var entity = await context.DoctorWorkSchedules.FindAsync(id)
                     ?? throw new KeyNotFoundException("WorkSchedule not found");
        return mapper.Map<WorkScheduleDto>(entity);
    }

    public async Task<WorkScheduleDto> UpdateWorkScheduleAsync(string id, UpdateWorkScheduleDto dto)
    {
        var entity = await context.DoctorWorkSchedules.FindAsync(id)
                     ?? throw new KeyNotFoundException("WorkSchedule not found");
        entity.Update(dto.StartTime, dto.EndTime);
        await context.SaveChangesAsync();
        return mapper.Map<WorkScheduleDto>(entity);
    }

    // Break
    public async Task<BreakDto> CreateBreakAsync(CreateDoctorBreakDto dto)
    {
        var entity = new DoctorBreak(dto.DoctorId, dto.StartTime, dto.EndTime, dto.IsRecurringWeekly);
        context.DoctorBreaks.Add(entity);
        await context.SaveChangesAsync();
        return mapper.Map<BreakDto>(entity);
    }

    public async Task<BreakDto> GetBreakByIdAsync(string id)
    {
        var entity = await context.DoctorBreaks.FindAsync(id)
                     ?? throw new KeyNotFoundException("Break not found");
        return mapper.Map<BreakDto>(entity);
    }

    // Unavailability
    public async Task<UnavailabilityDto> CreateUnavailabilityAsync(CreateUnavailabilityDto dto)
    {
        var entity = new DoctorUnavailability(dto.DoctorId, dto.StartTime, dto.EndTime);
        context.DoctorUnavailabilities.Add(entity);
        await context.SaveChangesAsync();
        return mapper.Map<UnavailabilityDto>(entity);
    }

    public async Task<UnavailabilityDto> GetUnavailabilityByIdAsync(string id)
    {
        var entity = await context.DoctorUnavailabilities.FindAsync(id)
                     ?? throw new KeyNotFoundException("Unavailability not found");
        return mapper.Map<UnavailabilityDto>(entity);
    }

    public async Task<DoctorCalendarDto> GetDoctorCalendarAsync(string appUserId)
    {
        var workSchedules = await context.DoctorWorkSchedules
            .Where(s => s.Doctor.AppUserId == appUserId)
            .ToListAsync();

        var breaks = await context.DoctorBreaks
            .Where(b => b.Doctor.AppUserId == appUserId)
            .ToListAsync();

        var unavailabilities = await context.DoctorUnavailabilities
            .Where(u => u.Doctor.AppUserId == appUserId)
            .ToListAsync();

        return new DoctorCalendarDto
        {
            WorkSchedules = mapper.Map<IEnumerable<WorkScheduleDto>>(workSchedules),
            Breaks = mapper.Map<IEnumerable<BreakDto>>(breaks),
            Unavailabilities = mapper.Map<IEnumerable<UnavailabilityDto>>(unavailabilities)
        };

    }

    public async Task DeleteWorkScheduleAsync(string id, string currentUserId)
    {
        var entity = await context.DoctorWorkSchedules
            .Include(e => e.Doctor) // AppUserId yoxlaması üçün
            .FirstOrDefaultAsync(e => e.Id == id)
            ?? throw new KeyNotFoundException("WorkSchedule not found");

        if (entity.Doctor.AppUserId != currentUserId)
            throw new UnauthorizedAccessException("You are not allowed to delete this work schedule");

        context.DoctorWorkSchedules.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteBreakAsync(string id, string currentUserId)
    {
        var entity = await context.DoctorBreaks
            .Include(e => e.Doctor)
            .FirstOrDefaultAsync(e => e.Id == id)
            ?? throw new KeyNotFoundException("Break not found");

        if (entity.Doctor.AppUserId != currentUserId)
            throw new UnauthorizedAccessException("You are not allowed to delete this break");

        context.DoctorBreaks.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteUnavailabilityAsync(string id, string currentUserId)
    {
        var entity = await context.DoctorUnavailabilities
            .Include(e => e.Doctor)
            .FirstOrDefaultAsync(e => e.Id == id)
            ?? throw new KeyNotFoundException("Unavailability not found");

        if (entity.Doctor.AppUserId != currentUserId)
            throw new UnauthorizedAccessException("You are not allowed to delete this unavailability");

        context.DoctorUnavailabilities.Remove(entity);
        await context.SaveChangesAsync();
    }




}
