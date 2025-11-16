namespace AppointmentSystem.Application.Common.Models.Identity;

public class ChangeTemporaryPasswordDto
{
    public string Email { get; set; } = null!;
    public string TemporaryPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}

