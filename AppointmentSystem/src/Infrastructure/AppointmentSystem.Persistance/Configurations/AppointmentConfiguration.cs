using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AppointmentSystem.Domain.Models;

namespace AppointmentSystem.Persistance.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DoctorId).IsRequired();
        builder.Property(x => x.PatientId).IsRequired();
        builder.Property(x => x.AvailabilityId).IsRequired();
        builder.Property(x => x.MedicalServiceId).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Notes).HasMaxLength(500);

        // Relationships
        builder.HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Availability)
            .WithMany()
            .HasForeignKey(a => a.AvailabilityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.MedicalService)
            .WithMany(ms => ms.Appointments)
            .HasForeignKey(a => a.MedicalServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(a => !a.IsDeleted);
    }

}
