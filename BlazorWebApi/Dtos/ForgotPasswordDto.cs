namespace BlazorWebApi.Dtos;

public class ForgotPasswordDto
{
    public string? ToEmail { get; set; } 

    public string? Subject { get; set; }

    public string? Body { get; set; }
}
