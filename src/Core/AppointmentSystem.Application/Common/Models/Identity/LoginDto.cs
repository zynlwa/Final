namespace AppointmentSystem.Application.Common.Models.Identity;

public class LoginDto
{

    public string? EmailOrUsername { get; init; }  // İstifadəçi həm email, həm username daxil edə bilər

    public string Password { get; init; } = null!;

    public bool RememberMe { get; init; } = false;
}
