namespace AppointmentSystem.Persistance.Configurations;
public class PromoCodeConfiguration : IEntityTypeConfiguration<PromoCode>
{
    public void Configure(EntityTypeBuilder<PromoCode> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Code)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(p => p.DiscountAmount)
               .HasColumnType("decimal(18,2)");

        builder.Property(p => p.DiscountPercent)
               .HasColumnType("decimal(5,2)");

        builder.Property(p => p.ExpirationDate)
               .IsRequired(false);
    }
}
