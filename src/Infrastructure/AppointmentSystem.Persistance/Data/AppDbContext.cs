
using AppointmentSystem.Domain.Common;
using AppointmentSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AppointmentSystem.Persistance.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options), IAppDbContext
{ 
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<MedicalService> MedicalServices { get; set; }
    public DbSet<Availability> Availabilities { get; set; }
    public DbSet<AppUser> Users { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<PromoCode> PromoCodes { get; set; }
    public DbSet<DoctorWorkSchedule> DoctorWorkSchedules { get; set; }
    public DbSet<DoctorBreak> DoctorBreaks { get; set; }
    public DbSet<DoctorUnavailability> DoctorUnavailabilities { get; set; }



    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Apply global filters for soft delete
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<bool>("IsDeleted")
                    .HasDefaultValue(false);
            }
        }
    }



}
