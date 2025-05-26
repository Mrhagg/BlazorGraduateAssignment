using BlazorWebApi.Dtos;
using BlazorWebApi.Interface;
using BlazorWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;


namespace BlazorWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IEmailService emailSender;
    private readonly UserManager<IdentityUser> userManager;
    private readonly IConfiguration _configuration;

    public AccountController(IEmailService emailSender, UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        this.emailSender = emailSender;
        this.userManager = userManager;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Welcome()
    {
        if (User.Identity == null || !User.Identity.IsAuthenticated)
        {
            return Ok("You are not authenticated");
        }

        return Ok("You are authenticated");
    }

    [Authorize]
    [HttpGet("Profile")]
    public async Task<IActionResult> Profile()
    {
        var currentUser = await userManager.GetUserAsync(User);
        {
            if (currentUser == null)
            {
                return BadRequest();
            }

            var userProfile = new UserProfile
            {
                Id = currentUser.Id,
                Name = currentUser.UserName ?? "",
                Email = currentUser.Email ?? "",
                PhoneNumber = currentUser.PhoneNumber ?? "",
            };

            return Ok(userProfile);
        }
    }

    [HttpPost("Forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
    {
        await emailSender.SendEmailAsync(model.ToEmail!, model.Subject!, model.Body!);
        return Ok("Email sent successfully!");
    }




    [HttpPost("Request-Password-Reset")]
    public async Task<IActionResult> RequestPasswordReset([FromBody] ForgotPasswordDto model)
    {
        var user = await userManager.FindByEmailAsync(model.ToEmail!);
        if (user == null)
        {

            return Ok();
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        var frontendUrl = _configuration["AppSettings:ClientUrl"];

        var resetLink = QueryHelpers.AddQueryString($"{frontendUrl}/reset-password", new Dictionary<string, string?>
        {
            ["email"] = model.ToEmail,
            ["token"] = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token))
        });

        var emailBody = $@"
            <html>
              <body style='font-family: Arial;'>
                <h2>Password Reset Request</h2>
                <p>Click the button below to reset your password:</p>
                <p><a href='{resetLink}' style='display:inline-block;padding:10px 20px;background-color:#007bff;color:white;text-decoration:none;border-radius:5px;'>Reset Password</a></p>
                <p>If you didn’t request this, you can ignore this email.</p>
              </body>
            </html>";

        await emailSender.SendEmailAsync(model.ToEmail!, "Password Reset", emailBody);

        return Ok("Password reset link sent!");
    }





    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return BadRequest("Invalid request.");

        var decodedTokenBytes = WebEncoders.Base64UrlDecode(model.Token);
        var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

        var result = await userManager.ResetPasswordAsync(user, decodedToken, model.NewPassword);

        if (result.Succeeded)
        {
            return Ok("Password has been reset!");
        }

        else
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return BadRequest(errors);
        }

    }


}
