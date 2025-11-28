namespace AppointmentSystem.Application.Common.Models.DoctorSchedule;

public record CreateWorkScheduleDto(string DoctorId, DayOfWeek DayOfWeek, DateTime  StartTime, DateTime  EndTime);
public record UpdateWorkScheduleDto(DateTime  StartTime, DateTime  EndTime);
public record WorkScheduleDto(string Id, string DoctorId, DayOfWeek DayOfWeek, DateTime  StartTime, DateTime  EndTime);
//break
public record CreateDoctorBreakDto(string DoctorId, DateTime StartTime, DateTime EndTime, bool IsRecurringWeekly);
public record BreakDto(string Id, string DoctorId, DateTime StartTime, DateTime EndTime, bool IsRecurringWeekly);

// Unavailability
public record CreateUnavailabilityDto(string DoctorId, DateTime StartTime, DateTime EndTime);
public record UnavailabilityDto(string Id, string DoctorId, DateTime StartTime, DateTime EndTime);
