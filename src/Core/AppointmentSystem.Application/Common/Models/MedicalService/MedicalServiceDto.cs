namespace AppointmentSystem.Application.Common.Models.MedicalService;

public record CreateMedicalServiceDto(
string Name,
string Description,
decimal Price,
string DoctorId
);

public record UpdateMedicalServiceDto(
    string Name,
    string Description,
    decimal Price
);

public record MedicalServiceDto(
    string Id,
    string Name,
    string Description,
    decimal Price,
    string DoctorId
);
