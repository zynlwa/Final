namespace AppointmentSystem.Application.Common.Models.Doctor;

public record DoctorDto(
    string Id,
    string FirstName,
    string LastName,
    string Specialty,
    string Email,
    string? ImageUrl,
    string? PhoneNumber,
    int ExperienceYears,
    double Rating,          // <- əlavə et
    int Patients,           // <- əlavə et
    bool IsDeleted,
    DateTime? DeletedAt,
    string? DeletedBy
);


public record CreateDoctorDto(
    string FirstName,
    string LastName,
    string Specialty,
    string Email,
    string? ImageUrl,
    string? PhoneNumber,
    int ExperienceYears           
);

public record UpdateDoctorDto(
    string FirstName,
    string LastName,
    string Specialty,
    string Email,
    string? ImageUrl,
    string? PhoneNumber,
    int ExperienceYears          
);
