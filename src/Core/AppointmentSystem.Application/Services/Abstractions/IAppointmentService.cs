namespace AppointmentSystem.Application.Services.Abstractions;

public interface IAppointmentService
{
    Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto dto);

    Task<AppointmentDto> ApproveAppointmentAsync(string appointmentId);

    Task<AppointmentDto> CancelAppointmentAsync(string appointmentId);

    Task<IEnumerable<AvailabilityDto>> GetAvailableSlotsAsync(string doctorId, DateTime date);

    Task<AppointmentDto?> GetAppointmentByIdAsync(string appointmentId);

    Task<IEnumerable<AppointmentDto>> GetAppointmentsForDoctorAsync(string doctorId);

    Task<IEnumerable<AppointmentDto>> GetAppointmentsForPatientAsync(string patientId);
}
