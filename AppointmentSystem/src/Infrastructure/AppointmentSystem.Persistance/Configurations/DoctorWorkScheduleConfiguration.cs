namespace AppointmentSystem.Persistance.Configurations;

public class DoctorWorkScheduleConfiguration : IEntityTypeConfiguration<DoctorWorkSchedule>
{
    public void Configure(EntityTypeBuilder<DoctorWorkSchedule> builder)
    {
        builder.HasOne(d => d.Doctor)
               .WithMany(d => d.WorkSchedules)
               .HasForeignKey(d => d.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(d => d.DayOfWeek)
               .IsRequired();

        builder.Property(d => d.StartTime)
               .IsRequired();

        builder.Property(d => d.EndTime)
               .IsRequired();
    }
}

