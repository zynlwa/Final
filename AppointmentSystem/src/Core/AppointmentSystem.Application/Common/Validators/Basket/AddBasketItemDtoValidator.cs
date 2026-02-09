namespace AppointmentSystem.Application.Common.Validators.Basket;
public class AddBasketItemDtoValidator : AbstractValidator<AddBasketItemDto>
{
    public AddBasketItemDtoValidator()
    {
        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("DoctorId is required.");

        RuleFor(x => x.AvailabilityId)
            .NotEmpty().WithMessage("AvailabilityId is required.");

        RuleFor(x => x.MedicalServiceId)
            .NotEmpty().WithMessage("MedicalServiceId is required.");
    }
}
