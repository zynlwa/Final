namespace AppointmentSystem.Application.Common.Models.PromoCode;

public record PromoCodeDto(
    string Id,
    string Code,
    decimal DiscountAmount,
    decimal DiscountPercent,
    DateTime? ExpirationDate,
    bool IsValid
);

public record CreatePromoCodeDto(
    string Code,
    decimal DiscountAmount,
    decimal DiscountPercent,
    DateTime? ExpirationDate
);

public record UpdatePromoCodeDto(
    string Code,
    decimal DiscountAmount,
    decimal DiscountPercent,
    DateTime? ExpirationDate
);
