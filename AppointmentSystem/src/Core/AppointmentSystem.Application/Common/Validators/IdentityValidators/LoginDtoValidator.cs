namespace AppointmentSystem.Application.Common.Validators.IdentityValidators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.EmailOrUsername)
            .NotEmpty().WithMessage("Email or username is required.")
            .Must(BeValidEmailOrUsername)
            .WithMessage("Invalid email or username format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }

    private bool BeValidEmailOrUsername(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (Regex.IsMatch(input, emailRegex, RegexOptions.IgnoreCase))
            return true;

       
        var usernameRegex = @"^[a-zA-Z0-9._-]{3,}$";
        return Regex.IsMatch(input, usernameRegex);
    }
}