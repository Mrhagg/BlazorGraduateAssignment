using System.ComponentModel.DataAnnotations;

namespace BlazorWebApi.Dtos;

public class FactionsDto
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? ImageUrl { get; set; }
}
