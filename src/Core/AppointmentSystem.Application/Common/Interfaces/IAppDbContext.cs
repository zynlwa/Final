namespace AppointmentSystem.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Doctor> Doctors { get; set; }
    DbSet<Appointment> Appointments { get;set; }
    DbSet<Patient> Patients { get; set; }
    DbSet<MedicalService> MedicalServices { get; set; }
    DbSet<Availability> Availabilities { get; set; }
    DbSet<AppUser> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
