
namespace AppointmentSystem.Application.Services.Abstractions;

public interface IPatientService
{
    Task<PatientDto> CreatePatientAsync(CreatePatientDto patientDto);
    Task<PatientDto> GetPatientByIdAsync(string id);
    Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
    Task UpdatePatientAsync(string id, UpdatePatientDto patientDto);
    Task SoftDeletePatientAsync(string id, string deletedBy);
}

