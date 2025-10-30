namespace AppointmentSystem.Application.Common.Models.Patient;

public record PatientDto(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    string? PhoneNumber,
    bool IsDeleted,
    DateTime? DeletedAt,
    string? DeletedBy
);

public record CreatePatientDto(
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    string? PhoneNumber
);


public record UpdatePatientDto(
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    string? PhoneNumber
);
