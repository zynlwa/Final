using AppointmentSystem.Application.Common.Models.Identity;
using FluentValidation;
using System.Text.RegularExpressions;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.EmailOrUsername)
            .NotEmpty().WithMessage("Email or Username is required.")
            .Must(BeValidEmailOrUsername).WithMessage("Invalid email or username format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.");
    }

    private bool BeValidEmailOrUsername(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        var isEmail = Regex.IsMatch(input, emailRegex, RegexOptions.IgnoreCase);

        var usernameRegex = @"^[a-zA-Z0-9._-]{3,}$";
        var isUsername = Regex.IsMatch(input, usernameRegex);

        return isEmail || isUsername;
    }
}
