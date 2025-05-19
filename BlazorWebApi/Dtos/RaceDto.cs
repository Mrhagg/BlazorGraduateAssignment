namespace BlazorWebApi.Dtos;

public class RaceDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImageFileName { get; set; }
    public string? Description { get; set; }

    public List<WowClassDto>? AllowedClasses { get; set; }
}
