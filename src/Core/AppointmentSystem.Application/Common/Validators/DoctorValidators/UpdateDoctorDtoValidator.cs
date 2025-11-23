namespace AppointmentSystem.Application.Validators.DoctorValidators;

public class UpdateDoctorDtoValidator : AbstractValidator<UpdateDoctorDto>
{
    public UpdateDoctorDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => x.Specialty)
            .NotEmpty().WithMessage("Specialty is required.")
            .MaximumLength(100).WithMessage("Specialty cannot exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?\d{10,15}$")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("Invalid phone number format.");

        RuleFor(x => x.ExperienceYears)
    .GreaterThanOrEqualTo(0)
    .When(x => x.ExperienceYears != null)
    .WithMessage("Experience years must be a non-negative integer.");


    }
}
