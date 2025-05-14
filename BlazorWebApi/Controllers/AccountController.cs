using BlazorWebApi.Dtos;
using BlazorWebApi.Interface;
using BlazorWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace BlazorWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IEmailSender emailSender, UserManager<IdentityUser> userManager) : ControllerBase
{
    private readonly UserManager<IdentityUser> userManager = userManager;

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
        var user = await userManager.FindByEmailAsync(model.Email!);
        if (user == null)
        {
            return Ok();
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = WebUtility.UrlEncode(token);

        var resetUrl = $"http://localhost:5178/ResetPassword?email={model.Email}&token={encodedToken}";


        await emailSender.SendEmailAsync(
            model.Email!,
            "Reset your password",
            $"Click the link to reset your password: <a href='{resetUrl}'>Reset Password</a>"
        );
        try
        {
            await emailSender.SendEmailAsync(
                model.Email!,
                "Reset your password",
                $"Click the link to reset your password: <a href='{resetUrl}'>Reset Password</a>"
            );
          Console.WriteLine($"Password reset link sent to {model.Email}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            return StatusCode(500, "Error sending email");
        }

        return Ok("Password reset token sent to email");
    }   

}
