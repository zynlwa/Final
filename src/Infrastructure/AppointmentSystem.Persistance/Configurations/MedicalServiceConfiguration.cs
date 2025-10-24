
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentSystem.Persistance.Configurations;

public class MedicalServiceConfiguration : IEntityTypeConfiguration<MedicalService>
{
    public void Configure(EntityTypeBuilder<MedicalService> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(x => x.Description)
            .HasMaxLength(1000);
        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        builder.HasMany(ms => ms.Appointments)
            .WithOne(a => a.MedicalService)
            .HasForeignKey(a => a.MedicalServiceId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasQueryFilter(p => !p.IsDeleted);

    }
}
