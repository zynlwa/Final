namespace AppointmentSystem.Application.Common.Models.Appointment;

public record CreateAppointmentDto(
      string DoctorId,
      string PatientId,
      string AvailabilityId,
      string MedicalServiceId,
      string? Notes
  );

public record AppointmentDto(
    string Id,
    string DoctorId,
    string DoctorName,
    string PatientId,
    string PatientName,
    string AvailabilityId,
    DateTime StartTime,
    DateTime EndTime,
    string MedicalServiceId,
    string MedicalServiceName,
    string Status,
    string? Notes
);

