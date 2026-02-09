using AppointmentSystem.Application.Common.Models.Doctor;

namespace AppointmentSystem.Application.Services.Abstractions;

public interface IDoctorService
{
    Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto doctorDto);
    Task<DoctorDto> GetDoctorByIdAsync(string id);
    Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
    Task UpdateDoctorAsync(string id, UpdateDoctorDto doctorDto);
    Task SoftDeleteDoctorAsync(string id, string deletedBy);
}
