namespace AppointmentSystem.Application.Services.Abstractions
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto dto);

        Task<AppointmentDto> ApproveAppointmentAsync(string appointmentId);

        Task<AppointmentDto> CancelAppointmentAsync(string appointmentId);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsForCurrentDoctorAsync(int page = 1, int pageSize = 20);

        Task<AppointmentDto?> GetAppointmentByIdAsync(string appointmentId);

        Task<IEnumerable<AppointmentDto>> GetAppointmentsForPatientAsync(string patientId, int page = 1, int pageSize = 20);
    }
}
