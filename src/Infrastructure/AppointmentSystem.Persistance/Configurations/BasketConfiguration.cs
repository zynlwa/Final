using AppointmentSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppointmentSystem.Persistance.Configurations;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        
        builder.HasKey(b => b.Id);

        builder.Property(b => b.PatientId)
               .IsRequired();

      
        builder.HasMany(b => b.Items)
               .WithOne(i => i.Basket)
               .HasForeignKey(i => i.BasketId)
               .OnDelete(DeleteBehavior.Cascade);

        
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

        builder.Property(i => i.MedicalServiceId)
               .IsRequired();

        builder.Property(i => i.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        // Relations
        builder.HasOne(i => i.Basket)
               .WithMany(b => b.Items)
               .HasForeignKey(i => i.BasketId);

        builder.HasOne(i => i.Doctor)
               .WithMany()
               .HasForeignKey(i => i.DoctorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.MedicalService)
               .WithMany()
               .HasForeignKey(i => i.MedicalServiceId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Availability)
        .WithMany()
        .HasForeignKey(i => i.AvailabilityId)
        .OnDelete(DeleteBehavior.Restrict);

    }
}
