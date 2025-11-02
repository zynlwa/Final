namespace AppointmentSystem.Application.Common.Models.Identity;

public record LoginResponseDto
{
    public string Token { get; init; } = null!;
    public UserDto User { get; init; } = null!;
    public DateTime ExpiresAt { get; init; }
}
