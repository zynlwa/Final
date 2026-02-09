namespace AppointmentSystem.Persistance.Configurations;

public class DoctorUnavailabilityConfiguration : IEntityTypeConfiguration<DoctorUnavailability>
{
    public void Configure(EntityTypeBuilder<DoctorUnavailability> builder)
    {
        builder.HasOne(d => d.Doctor)
               .WithMany(d => d.Unavailabilities)
               .HasForeignKey(d => d.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
