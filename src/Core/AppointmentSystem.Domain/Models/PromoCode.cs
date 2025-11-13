namespace AppointmentSystem.Domain.Entities;

public class PromoCode : BaseEntity
{
    private PromoCode() { } // EF Core üçün

    public PromoCode(string code, decimal discountAmount, decimal discountPercent, DateTime? expirationDate = null)
    {
        Code = code;
        DiscountAmount = discountAmount;
        DiscountPercent = discountPercent;
        ExpirationDate = expirationDate;
    }

    public string Code { get; private set; } = null!;
    public decimal DiscountAmount { get; private set; }
    public decimal DiscountPercent { get; private set; }
    public DateTime? ExpirationDate { get; private set; }

    public bool IsValid() => !ExpirationDate.HasValue || ExpirationDate > DateTime.UtcNow;

    public void Update(string code, decimal discountAmount, decimal discountPercent, DateTime? expirationDate = null)
    {
        Code = code;
        DiscountAmount = discountAmount;
        DiscountPercent = discountPercent;
        ExpirationDate = expirationDate;
    }
}
