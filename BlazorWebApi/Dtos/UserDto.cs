using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorWebApi.Dtos;

public class UserDto
{
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; } = "";

    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; } = "";

    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must contain at least 8 characters, one uppercase letter, one lowercase letter, and one number.")]
    public string Password { get; set; } = "";

    public string GetHashedPassword()
    {
        return BCrypt.Net.BCrypt.HashPassword(Password);
    }
}
