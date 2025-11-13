using AppointmentSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentSystem.Persistance.Configurations;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        // Primary key
        builder.HasKey(b => b.Id);

        // PatientId mütləq olmalıdır
        builder.Property(b => b.PatientId)
               .IsRequired();

        // Basket-ə aid olan item-ları map et
        builder.HasMany(b => b.Items)
               .WithOne(i => i.Basket)
               .HasForeignKey(i => i.BasketId)
               .OnDelete(DeleteBehavior.Cascade);

        // Optional: CreatedAt, UpdatedAt default dəyərlər varsa
        builder.Property(b => b.CreatedAt)
               .HasDefaultValueSql("GETUTCDATE()");
    }
}

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.BasketId)
               .IsRequired();

        builder.Property(i => i.DoctorId)
               .IsRequired();

        builder.Property(i => i.AvailabilityId)
               .IsRequired();

        builder.Property(i => i.MedicalServiceId)
               .IsRequired();
    }
}
