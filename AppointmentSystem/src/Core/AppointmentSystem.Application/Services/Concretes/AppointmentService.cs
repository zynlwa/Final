using AppointmentSystem.Domain.Enums;

namespace AppointmentSystem.Application.Services.Concretes;

public class AppointmentService : IAppointmentService
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    private readonly Abstractions.ISlotService _slotService;
    private readonly ICurrentUserService _currentUserService;

    public AppointmentService(IAppDbContext context, IMapper mapper, Abstractions.ISlotService slotService, ICurrentUserService currentUserService  )
    {
        _context = context;
        _mapper = mapper;
        _slotService = slotService;
        _currentUserService = currentUserService;
    }

    public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto dto)
    {
        // 1️⃣ Check availability
        var availability = await _context.Availabilities
            .Include(a => a.Doctor)
            .Include(a => a.MedicalService)
            .FirstOrDefaultAsync(a => a.Id == dto.AvailabilityId && a.DoctorId == dto.DoctorId);

        if (availability == null)
            throw new InvalidOperationException("Selected time slot is not available for this doctor.");

        if (availability.IsBooked)
            throw new InvalidOperationException("This time slot has already been booked.");

        // 2️⃣ Get all doctor breaks (client-side filtering)
        var allDoctorBreaks = await _context.DoctorBreaks
      .Where(b => b.DoctorId == dto.DoctorId) // SQL-friendly
      .ToListAsync();

        var doctorBreaks = allDoctorBreaks
            .Where(b =>
                b.StartTime.Date == availability.StartTime.Date ||
                (b.IsRecurringWeekly && b.StartTime.DayOfWeek == availability.StartTime.DayOfWeek))
            .ToList();


        if (doctorBreaks.Any(b => b.StartTime < availability.EndTime && b.EndTime > availability.StartTime))
            throw new InvalidOperationException("Doctor is on break at this time.");

        // 3️⃣ Generate slots
        var generatedSlots = await _slotService.GenerateSlotsAsync(
            dto.DoctorId,
            availability.StartTime.Date,
            availability.MedicalServiceId
        );

        var overlappingDoctorSlot = generatedSlots.Any(s =>
            s.StartTime < availability.EndTime && s.EndTime > availability.StartTime && s.IsBooked);

        if (overlappingDoctorSlot)
            throw new InvalidOperationException("Doctor has another appointment or is unavailable at this time.");

        // 4️⃣ Check patient overlapping appointments
        var patientAppointments = await _context.Appointments
            .Include(a => a.Availability)
            .Where(a => a.PatientId == dto.PatientId && a.Status != AppointmentStatus.Cancelled)
            .ToListAsync();

        if (patientAppointments.Any(a =>
            a.Availability.StartTime < availability.EndTime &&
            a.Availability.EndTime > availability.StartTime))
        {
            throw new InvalidOperationException("You have another appointment at this time.");
        }

        // 5️⃣ Create appointment
        var appointment = new Appointment(
            dto.DoctorId,
            dto.PatientId,
            dto.AvailabilityId,
            availability.MedicalServiceId,
            dto.Notes
        );

        _context.Appointments.Add(appointment);

        // 6️⃣ Book availability
        availability.Book();

        await _context.SaveChangesAsync();

        // 7️⃣ Load related entities
        appointment = await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.MedicalService)
            .Include(a => a.Availability)
            .FirstAsync(a => a.Id == appointment.Id);

        return _mapper.Map<AppointmentDto>(appointment);
    }


    public async Task<AppointmentDto> ApproveAppointmentAsync(string appointmentId)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Availability)
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.MedicalService)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);

        if (appointment == null)
            throw new KeyNotFoundException("Appointment not found.");

        if (appointment.Status == AppointmentStatus.Cancelled)
            throw new InvalidOperationException("Cannot approve a cancelled appointment.");

        if (appointment.Status == AppointmentStatus.Confirmed)
            throw new InvalidOperationException("Appointment is already confirmed.");

        appointment.Approve();
        await _context.SaveChangesAsync();

        return _mapper.Map<AppointmentDto>(appointment);
    }

    public async Task<AppointmentDto> CancelAppointmentAsync(string appointmentId)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Availability)
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.MedicalService)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);

        if (appointment == null)
            throw new KeyNotFoundException("Appointment not found.");

        if (appointment.Status == AppointmentStatus.Cancelled)
            throw new InvalidOperationException("Appointment is already cancelled.");

        appointment.Cancel();
        await _context.SaveChangesAsync();

        return _mapper.Map<AppointmentDto>(appointment);
    }

    public async Task<AppointmentDto?> GetAppointmentByIdAsync(string appointmentId)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Availability)
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.MedicalService)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);

        return appointment == null ? null : _mapper.Map<AppointmentDto>(appointment);
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsForPatientAsync(string patientId, int page = 1, int pageSize = 20)
    {
        var appointments = await _context.Appointments
            .Include(a => a.Availability)
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.MedicalService)
            .Where(a => a.PatientId == patientId)
            .OrderBy(a => a.Availability.StartTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
    }

    public async Task<IEnumerable<AppointmentDto>>GetAppointmentsForCurrentDoctorAsync(int page = 1, int pageSize = 20)
    {
        var userId = _currentUserService.UserId;

        var doctor = await _context.Doctors
            .FirstOrDefaultAsync(d => d.AppUserId == userId);

        if (doctor == null)
            return Enumerable.Empty<AppointmentDto>();

        var appointments = await _context.Appointments
            .Include(a => a.Availability)
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .Include(a => a.MedicalService)
            .Where(a => a.DoctorId == doctor.Id)
            .OrderBy(a => a.Availability.StartTime)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        foreach (var a in appointments)
        {
            a.SetMissed();
        }

        await _context.SaveChangesAsync();

        return appointments.Select(a => new AppointmentDto(
            a.Id,
            a.DoctorId,
            $"{a.Doctor.FirstName} {a.Doctor.LastName}",
            a.PatientId,
            $"{a.Patient.FirstName} {a.Patient.LastName}",
            a.AvailabilityId,
            a.Availability.StartTime,
            a.Availability.EndTime,
            a.MedicalServiceId,
            a.MedicalService.Name,
            a.Status.ToString(),
            a.Notes
        ));
    }

}
