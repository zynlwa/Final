namespace AppointmentSystem.Application.Common.Validators.IdentityValidators;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.FirstName))
            .WithMessage("First name cannot exceed 50 characters.");

        RuleFor(x => x.LastName)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.LastName))
            .WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?\d{7,15}$")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("Phone number must contain 7–15 digits and may start with '+'.");

        RuleFor(x => x.UserName)
            .Matches(@"^[a-zA-Z0-9._-]{3,}$")
            .When(x => !string.IsNullOrWhiteSpace(x.UserName))
            .WithMessage("Username can contain letters, numbers, '.', '_', '-' and must be at least 3 characters long.");
    }
}
