using AppointmentSystem.Application.Common.Models.DoctorSchedule;

namespace AppointmentSystem.Application.Services.Concretes;

public class DoctorScheduleService(IAppDbContext context, IMapper mapper) : IDoctorScheduleService
{

    public async Task<IEnumerable<WorkScheduleDto>> GetWorkSchedulesForDoctorAsync(string doctorId)
    {
        var schedules = await context.DoctorWorkSchedules
            .Where(s => s.DoctorId == doctorId)
            .ToListAsync();

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
}
