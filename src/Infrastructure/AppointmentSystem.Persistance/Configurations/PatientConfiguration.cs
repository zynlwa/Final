using AppointmentSystem.Domain.Entities;

namespace AppointmentSystem.Persistance.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
       builder.HasKey(x=> x.Id);
        builder.Property(x=>x.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x=>x.LastName)
            .IsRequired()
            .HasMaxLength(150);
        builder.Property(x => x.DateOfBirth)
            .IsRequired();
        builder.Property(x=>x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasMany(p => p.Appointments)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
