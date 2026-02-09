namespace AppointmentSystem.Application.Common.Models.Identity;
public record UpdateUserDto
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? UserName { get; init; }
}

