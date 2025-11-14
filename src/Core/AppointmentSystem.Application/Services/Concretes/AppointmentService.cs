using AppointmentSystem.Domain.Entities;
using AppointmentSystem.Domain.Enums;

namespace AppointmentSystem.Application.Services.Concretes;

public class AppointmentService(IAppDbContext context, IMapper mapper) : IAppointmentService
{
    public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto dto)
    {
        // 1. Availabilty tap
        var availability = await context.Availabilities
            .FirstOrDefaultAsync(a => a.Id == dto.AvailabilityId && a.DoctorId == dto.DoctorId);

        if (availability == null)
            throw new InvalidOperationException($"Availability with ID '{dto.AvailabilityId}' for Doctor '{dto.DoctorId}' not found.");

        if (availability.StartTime < DateTime.UtcNow)
            throw new InvalidOperationException("Cannot book an availability in the past.");

        // Already booked?
        if (availability.IsBooked)
            throw new InvalidOperationException("This availability slot is already booked.");

        // 2. Medical service yoxla
        var service = await context.MedicalServices
            .FirstOrDefaultAsync(s => s.Id == dto.MedicalServiceId);

        if (service == null)
            throw new InvalidOperationException($"Medical service '{dto.MedicalServiceId}' not found.");

        if (service.DoctorId != dto.DoctorId)
            throw new InvalidOperationException("Selected medical service does not belong to this doctor.");

        // 3. Same availability üçün ikinci appointment yaratmaq olmaz
        var exists = await context.Appointments
            .AnyAsync(a => a.AvailabilityId == dto.AvailabilityId &&
                           a.Status != AppointmentStatus.Cancelled);

        if (exists)
            throw new InvalidOperationException("You cannot create two appointments for the same time slot.");

        // 4. Patient overlapping check
        var overlappingPatient = await context.Appointments
            .AnyAsync(a => a.PatientId == dto.PatientId &&
                           a.Availability.StartTime == availability.StartTime &&
                           a.Status != AppointmentStatus.Cancelled);

        if (overlappingPatient)
            throw new InvalidOperationException("Patient already has an appointment at this time.");

        // 5. Doctor overlapping check
        var overlappingDoctor = await context.Appointments
            .AnyAsync(a => a.DoctorId == dto.DoctorId &&
                           a.Availability.StartTime == availability.StartTime &&
                           a.Status != AppointmentStatus.Cancelled);

        if (overlappingDoctor)
            throw new InvalidOperationException("Doctor already has an appointment at this time.");

        // 6. Appointment yarat
        var appointment = mapper.Map<Appointment>(dto);
        context.Appointments.Add(appointment);

        // 7. Availability-ni book et
        availability.Book();

        await context.SaveChangesAsync();

        // 8. Full include ilə appointment qaytar
        appointment = await context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.Availability)
            .Include(a => a.MedicalService)
            .FirstOrDefaultAsync(a => a.Id == appointment.Id);

        return mapper.Map<AppointmentDto>(appointment);
    }


    public async Task<AppointmentDto> ApproveAppointmentAsync(string appointmentId)
    {
        var appointment = await context.Appointments
            .Include(a => a.Availability)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);

        if (appointment == null)
            throw new KeyNotFoundException("Appointment not found.");

        // Domain metodunu istifadə edərək təsdiqlə
        appointment.Approve();

        await context.SaveChangesAsync();

        // Full include ilə DTO qaytar
        appointment = await context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.MedicalService)
            .Include(a => a.Availability)
            .FirstOrDefaultAsync(a => a.Id == appointment.Id);

        return mapper.Map<AppointmentDto>(appointment);
    }


    public async Task<AppointmentDto> CancelAppointmentAsync(string appointmentId)
    {
        var appointment = await context.Appointments
            .Include(a => a.Availability)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);

        if (appointment == null)
            throw new KeyNotFoundException("Appointment not found.");

        appointment.Cancel();
        await context.SaveChangesAsync();

        return mapper.Map<AppointmentDto>(appointment);
    }

    public async Task<AppointmentDto?> GetAppointmentByIdAsync(string appointmentId)
    {
        var appointment = await context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.MedicalService)
            .Include(a => a.Availability)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);

        return appointment == null ? null : mapper.Map<AppointmentDto>(appointment);
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsForDoctorAsync(string doctorId)
    {
        var appointments = await context.Appointments
     .Include(a => a.Doctor)
     .Include(a => a.Patient)
     .Include(a => a.Availability)
     .Include(a => a.MedicalService)
     .Where(a => a.DoctorId == doctorId)
     .ToListAsync();


        return mapper.Map<IEnumerable<AppointmentDto>>(appointments);
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsForPatientAsync(string patientId)
    {
        var appointments = await context.Appointments
     .Include(a => a.Doctor)
     .Include(a => a.Patient)
     .Include(a => a.Availability)
     .Include(a => a.MedicalService)
     .Where(a => a.PatientId == patientId)
     .ToListAsync();

        return mapper.Map<IEnumerable<AppointmentDto>>(appointments);
    }

    public async Task<IEnumerable<AvailabilityDto>> GetAvailableSlotsAsync(string doctorId, DateTime date)
    {
        var slots = await context.Availabilities
            .Where(a => a.DoctorId == doctorId && a.StartTime.Date == date.Date && !a.IsBooked)
            .ToListAsync();

        return mapper.Map<IEnumerable<AvailabilityDto>>(slots);
    }
}
