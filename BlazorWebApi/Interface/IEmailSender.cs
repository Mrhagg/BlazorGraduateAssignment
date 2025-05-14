namespace BlazorWebApi.Interface;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string message, string subject);
}