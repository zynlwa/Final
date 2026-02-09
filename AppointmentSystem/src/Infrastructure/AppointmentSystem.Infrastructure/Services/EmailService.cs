
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;
using AppointmentSystem.Application.Common.Interfaces;

namespace AppointmentSystem.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendEmail(string toEmail, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:From"]));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = body };

        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        smtp.Connect(
            _configuration["EmailSettings:SmtpHost"],
            int.Parse(_configuration["EmailSettings:SmtpPort"]),
            SecureSocketOptions.StartTls
        );
        smtp.Authenticate(
            _configuration["EmailSettings:Username"],
            _configuration["EmailSettings:Password"]
        );
        smtp.Send(email);
        smtp.Disconnect(true);
    }

    public Task SendTemporaryPasswordAsync(string toEmail, string tempPassword)
    {
        string subject = "Your Temporary Password";
        string body = $"<p>Hello,</p><p>Your temporary password is: <strong>{tempPassword}</strong></p><p>Please change it after first login.</p>";

        SendEmail(toEmail, subject, body);
        return Task.CompletedTask;
    }
}
