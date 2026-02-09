namespace AppointmentSystem.Application.Common.Models.Identity;

public class ChangePasswordDto
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}

