namespace AppointmentSystem.Application.Common.Models.Identity;

public record RegisterDto
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string ConfirmPassword { get; init; } = null!;
    public string? PhoneNumber { get; init; }
    public string Role { get; set; } = "Patient";
}
