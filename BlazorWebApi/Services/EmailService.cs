using BlazorWebApi.Interface;
using FluentEmail.Core;

namespace BlazorWebApi.Services;

public class EmailService : IEmailService
{
   private readonly IFluentEmail _fluentEmail;

    public EmailService(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }
    

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        await _fluentEmail
           .To(toEmail)
           .Subject(subject)
           .Body(body, isHtml: true)
           .SendAsync();
    }
}
