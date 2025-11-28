
public class Appointment : BaseEntity
{
    private Appointment() { }

    public Appointment(string doctorId, string patientId, string availabilityId, string medicalServiceId, string? notes = null)
    {
        DoctorId = doctorId;
        PatientId = patientId;
        AvailabilityId = availabilityId;
        MedicalServiceId = medicalServiceId;
        Notes = notes;
        Status = AppointmentStatus.Pending;
    }

    public string DoctorId { get; private set; }
    public Doctor Doctor { get; private set; } = null!;

    public string PatientId { get; private set; }
    public Patient Patient { get; private set; } = null!;

    public string AvailabilityId { get; private set; }
    public Availability Availability { get; private set; } = null!;

    public string MedicalServiceId { get; private set; }
    public MedicalService MedicalService { get; private set; } = null!;

    public AppointmentStatus Status { get; private set; }= AppointmentStatus.Pending;
    public string? Notes { get; private set; }
    public void Approve()
    {
        if (Status == AppointmentStatus.Cancelled)
            throw new InvalidOperationException("Cannot approve a cancelled appointment.");

        // Availability artıq booked olubsa, sadəcə status dəyiş
        Status = AppointmentStatus.Confirmed;
    }


    public void Cancel()
    {
        if (Status == AppointmentStatus.Cancelled)
            throw new InvalidOperationException("Appointment is already cancelled.");

        Status = AppointmentStatus.Cancelled;
        Availability.Cancel();
    }
    public void SetMissed()
    {
        if (Status == AppointmentStatus.Pending && Availability.StartTime < DateTime.UtcNow)
            Status = AppointmentStatus.Missed;
    }

    public void AddNotes(string notes)
    {
        if (string.IsNullOrWhiteSpace(notes))
            throw new ArgumentException("Notes cannot be empty.");

        Notes = notes;
    }
}
