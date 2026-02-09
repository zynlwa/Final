namespace AppointmentSystem.Application.Common.Models.Availability;

public record CreateAvailabilityDto(
    string DoctorId,
    string MedicalServiceId,
    DateTime StartTime,
    DateTime EndTime
);

public record AvailabilityDto(
    string Id,
    string DoctorId,
    string MedicalServiceId,
    DateTime StartTime,
    DateTime EndTime,
    bool IsBooked,
    DateTime CreatedAt
);
