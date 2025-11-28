using AppointmentSystem.Application.Common.Models.DoctorSchedule;

namespace AppointmentSystem.Application.Services.Abstractions;

public interface IDoctorScheduleService
{
    // WorkSchedule
    Task<IEnumerable<WorkScheduleDto>> GetWorkSchedulesForDoctorAsync(string doctorId, bool byAppUserId = false);
    Task<WorkScheduleDto> CreateWorkScheduleAsync(CreateWorkScheduleDto dto);
    Task<WorkScheduleDto> GetWorkScheduleByIdAsync(string id);
    Task<WorkScheduleDto> UpdateWorkScheduleAsync(string id, UpdateWorkScheduleDto dto);

    // Break
    Task<BreakDto> CreateBreakAsync(CreateDoctorBreakDto dto);
    Task<BreakDto> GetBreakByIdAsync(string id);

    // Unavailability
    Task<UnavailabilityDto> CreateUnavailabilityAsync(CreateUnavailabilityDto dto);
    Task<UnavailabilityDto> GetUnavailabilityByIdAsync(string id);

    Task<DoctorCalendarDto> GetDoctorCalendarAsync(string appUserId);
    Task DeleteWorkScheduleAsync(string id, string currentUserId);
    Task DeleteBreakAsync(string id, string currentUserId);
    Task DeleteUnavailabilityAsync(string id, string currentUserId);
}
