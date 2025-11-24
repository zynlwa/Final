namespace AppointmentSystem.Application.Services.Abstractions;
public interface IAvailabilityService
{
        Task<AvailabilityDto> CreateAvailabilityAsync(CreateAvailabilityDto dto);
        Task<IEnumerable<AvailabilityDto>> GetAvailableSlotsAsync(string doctorId, DateTime date);
        Task<AvailabilityDto?> GetAvailabilityByIdAsync(string availabilityId);
        Task CancelAvailabilityAsync(string availabilityId);
        Task<IEnumerable<AvailabilityDto>> GetAvailabilityForDoctorAsync(string doctorId, DateTime? date = null);
}

