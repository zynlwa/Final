namespace AppointmentSystem.Application.Validators.MedicalServiceValidators;

public class UpdateMedicalServiceDtoValidator : AbstractValidator<UpdateMedicalServiceDto>
{
    public UpdateMedicalServiceDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Service name is required.")
            .MaximumLength(100).WithMessage("Service name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}
