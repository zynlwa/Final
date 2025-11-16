
namespace AppointmentSystem.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendTemporaryPasswordAsync(string toEmail, string tempPassword);
    void SendEmail(string toEmail, string subject, string body);
}
