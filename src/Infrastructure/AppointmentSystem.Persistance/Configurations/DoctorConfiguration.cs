namespace AppointmentSystem.Persistance.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Specialty)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(150);
        builder.Property(c => c.ImageUrl).IsRequired(true).HasMaxLength(200);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20);

        
        builder.HasOne(d => d.AppUser)
            .WithOne()
            .HasForeignKey<Doctor>(d => d.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);

       
        builder.HasMany(d => d.Appointments)
            .WithOne(a => a.Doctor)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);


        
        builder.HasMany(d => d.Availabilities)
            .WithOne(a => a.Doctor)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        
        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
