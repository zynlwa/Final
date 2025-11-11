namespace AppointmentSystem.Application.Common.Models.Patient;

public record PatientDto(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    string? PhoneNumber,
    string AppUserId,
    bool IsDeleted,
    DateTime? DeletedAt,
    string? DeletedBy
);

public record CreatePatientDto(
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    string? PhoneNumber,
    string Password
    );

public record UpdatePatientDto(
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    string? PhoneNumber
);