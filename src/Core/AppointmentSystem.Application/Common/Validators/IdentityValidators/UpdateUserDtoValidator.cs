using AppointmentSystem.Application.Common.Models.Identity;
using FluentValidation;
public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .MaximumLength(50).When(x => !string.IsNullOrWhiteSpace(x.FirstName));

        RuleFor(x => x.LastName)
            .MaximumLength(50).When(x => !string.IsNullOrWhiteSpace(x.LastName));

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?\d{7,15}$").When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("Invalid phone number format.");

        RuleFor(x => x.UserName)
            .Matches(@"^[a-zA-Z0-9._-]{3,}$")
            .When(x => !string.IsNullOrWhiteSpace(x.UserName))
            .WithMessage("Username must be valid.");
    }
}
