namespace AppointmentSystem.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Doctor> Doctors { get; set; }
    DbSet<Appointment> Appointments { get;set; }
    DbSet<Patient> Patients { get; set; }
    DbSet<MedicalService> MedicalServices { get; set; }
    DbSet<Availability> Availabilities { get; set; }
    DbSet<AppUser> Users { get; set; }
    DbSet<Basket> Baskets { get; set; }
    DbSet<BasketItem> BasketItems { get; set; }
    DbSet<PromoCode> PromoCodes { get; set; }
    DbSet<DoctorWorkSchedule> DoctorWorkSchedules { get; set; }
    DbSet<DoctorBreak> DoctorBreaks { get; set; }
    DbSet<DoctorUnavailability> DoctorUnavailabilities { get; set; }
    DbSet<Review> Reviews { get; set; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}
