namespace AppointmentSystem.Application.Common.Models.DoctorSchedule;

public class DoctorCalendarDto
{
    public IEnumerable<WorkScheduleDto> WorkSchedules { get; set; } = new List<WorkScheduleDto>();
    public IEnumerable<BreakDto> Breaks { get; set; } = new List<BreakDto>();
    public IEnumerable<UnavailabilityDto> Unavailabilities { get; set; } = new List<UnavailabilityDto>();
}
