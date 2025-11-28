namespace AppointmentSystem.Application.Services.Abstractions
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto dto);

        Task<AppointmentDto> ApproveAppointmentAsync(string appointmentId);

        Task<AppointmentDto> CancelAppointmentAsync(string appointmentId);

        Task<AppointmentDto?> GetAppointmentByIdAsync(string appointmentId);

        Task<IEnumerable<AppointmentDto>> GetAppointmentsForDoctorByUserIdAsync(string userId, int page = 1, int pageSize = 20);

        Task<IEnumerable<AppointmentDto>> GetAppointmentsForPatientAsync(string patientId, int page = 1, int pageSize = 20);
    }
}
