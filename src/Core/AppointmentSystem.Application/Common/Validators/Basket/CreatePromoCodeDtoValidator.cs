namespace AppointmentSystem.Application.Common.Validators.Basket;
public class CreatePromoCodeDtoValidator : AbstractValidator<CreatePromoCodeDto>
{
    public CreatePromoCodeDtoValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Promo code is required.")
            .MaximumLength(50).WithMessage("Promo code cannot exceed 50 characters.");

        RuleFor(x => x.DiscountAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount amount must be greater than or equal to 0.");

        RuleFor(x => x.DiscountPercent)
            .InclusiveBetween(0, 100).WithMessage("Discount percent must be between 0 and 100.");
    }
}
