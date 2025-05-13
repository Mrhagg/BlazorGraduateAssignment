using System.ComponentModel.DataAnnotations;

namespace BlazorWebApi.Dtos;

public class FactionsDto
{
    [Required(ErrorMessage ="Choose a faction is required!")]
    public string? Name { get; set; }

    public string? ImageUrl { get; set; }
}
