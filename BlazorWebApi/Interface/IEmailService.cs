namespace BlazorWebApi.Interface;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string body, string subject);
}