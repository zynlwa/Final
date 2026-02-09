namespace AppointmentSystem.Application.Services.Abstractions;

public interface IMedicalServiceService
{
    Task<MedicalServiceDto> CreateMedicalServiceAsync(CreateMedicalServiceDto dto);
    Task<IEnumerable<MedicalServiceDto>> GetAllMedicalServicesAsync();
    Task<MedicalServiceDto?> GetMedicalServiceByIdAsync(string id);
    Task<IEnumerable<MedicalServiceDto>> GetMedicalServicesForDoctorAsync(string appUserId);
    Task UpdateMedicalServiceAsync(string id, UpdateMedicalServiceDto dto);
    Task DeleteMedicalServiceAsync(string id);

}
