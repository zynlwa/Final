using AppointmentSystem.Application.Common.Models.PromoCode;
using FluentValidation;

namespace AppointmentSystem.Application.Validators.PromoCodeValidators
{
    public class CreatePromoCodeDtoValidator : AbstractValidator<CreatePromoCodeDto>
    {
        public CreatePromoCodeDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Promo code is required.")
                .MaximumLength(50).WithMessage("Promo code must not exceed 50 characters.");

            RuleFor(x => x.DiscountAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount amount cannot be negative.");

            RuleFor(x => x.DiscountPercent)
                .InclusiveBetween(0, 100).WithMessage("Discount percent must be between 0 and 100.");

            RuleFor(x => x.ExpirationDate)
                .Must(date => !date.HasValue || date.Value > DateTime.UtcNow)
                .WithMessage("Expiration date must be in the future if specified.");
        }
    }

    public class UpdatePromoCodeDtoValidator : AbstractValidator<UpdatePromoCodeDto>
    {
        public UpdatePromoCodeDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Promo code is required.")
                .MaximumLength(50).WithMessage("Promo code must not exceed 50 characters.");

            RuleFor(x => x.DiscountAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount amount cannot be negative.");

            RuleFor(x => x.DiscountPercent)
                .InclusiveBetween(0, 100).WithMessage("Discount percent must be between 0 and 100.");

            RuleFor(x => x.ExpirationDate)
                .Must(date => !date.HasValue || date.Value > DateTime.UtcNow)
                .WithMessage("Expiration date must be in the future if specified.");
        }
    }
}
