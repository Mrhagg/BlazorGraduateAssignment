using BlazorWebApi.Interface;
using System.Net.Mail;
using System.Net;

namespace BlazorWebApi.Services;

public class SmtpEmailSender : IEmailSender
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;

    public SmtpEmailSender(IConfiguration configuration)
    {
        _smtpHost = configuration["EmailSettings:SmtpHost"]!;
        _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]!);
        _smtpUser = configuration["EmailSettings:SmtpUsername"]!;
        _smtpPass = configuration["EmailSettings:SmtpPassword"]!;
    }
    

    public async Task SendEmailAsync(string email, string message, string subject)
    {
        using (var smtpClient = new SmtpClient(_smtpHost, _smtpPort))
        {
            smtpClient.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
            smtpClient.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress("no-reply@yourdomain.com"),
                Subject = subject, 
                Body = message, 
                IsBodyHtml = true 
            };

           
            mailMessage.To.Add("1fb985c7b0-bfa896+1@inbox.mailtrap.io"); 

          
            Console.WriteLine("E-post skickad till Mailtrap!");
        }
        
    }
}
