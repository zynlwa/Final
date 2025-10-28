namespace AppointmentSystem.Application.Common.Models.Availability;

public record CreateAvailabilityDto(
        string DoctorId,
        DateTime StartTime,
        DateTime EndTime
    );

public record AvailabilityDto(
    string Id,
    string DoctorId,
    DateTime StartTime,
    DateTime EndTime,
    bool IsBooked,
    DateTime CreatedAt
);