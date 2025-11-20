namespace AppointmentSystem.Persistance.Configurations;

public class AvailabilityConfiguration : IEntityTypeConfiguration<Availability>
{
    public void Configure(EntityTypeBuilder<Availability> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.StartTime)
            .IsRequired();
        builder.Property(x => x.EndTime)
            .IsRequired();
        builder.Property(x => x.IsBooked)
            .IsRequired();
        
        builder.HasQueryFilter(p => !p.IsDeleted);
       //// builder.HasOne(a => a.Doctor)
       //      .WithMany(d => d.Availabilities)
       //      .HasForeignKey(a => a.DoctorId)
       //      .OnDelete(DeleteBehavior.Restrict);

        // Relation with MedicalService
        builder.HasOne(a => a.MedicalService)
            .WithMany(m => m.Availabilities)
            .HasForeignKey(a => a.MedicalServiceId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
