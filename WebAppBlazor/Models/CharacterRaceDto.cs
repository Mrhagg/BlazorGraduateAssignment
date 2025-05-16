namespace WebAppBlazor.Models;

public class CharacterRaceDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public string? ImageUrl { get; set; }
    public List<string>? RacialAbilities { get; set; } 

    public List<ClassDto>? AllowedClasses { get; set; }
}
