namespace LearnIn.Application.Interfaces.SMTP;

public interface IEmailService
{
    Task SendEmailAsync(string subject, string body, string email);
}
