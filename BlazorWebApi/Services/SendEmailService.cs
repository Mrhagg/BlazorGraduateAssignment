using BlazorWebApi.Interface;
namespace BlazorWebApi.Services;


public class SendEmailService(IEmailSender emailSender)
{
    private readonly IEmailSender emailSender = emailSender;


    public async Task SendPasswordResetEmail(string email, string token)
    {
        var resetUrl = $"http://localhost:5178/ResetPassword?email={email}&token={token}";


        await emailSender.SendEmailAsync(email, "Reset Password", $"Please reset your password by clicking here: {resetUrl}");
    }
}
