namespace AppointmentSystem.Persistance.Configurations;

public class DoctorBreakConfiguration : IEntityTypeConfiguration<DoctorBreak>
{
    public void Configure(EntityTypeBuilder<DoctorBreak> builder)
    {
        builder.HasOne(d => d.Doctor)
               .WithMany(d => d.Breaks)
               .HasForeignKey(d => d.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(d => d.IsRecurringWeekly)
               .IsRequired();
    }
}
