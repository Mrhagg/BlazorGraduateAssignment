namespace WebAppBlazor.Models;

public class CharacterRaceDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public string? ImageFileName { get; set; }
    public string ImageUrl => $"images/races/{(string.IsNullOrEmpty(ImageFileName) ? Name!.Replace(" ", "").ToLower() + ".jpg" : ImageFileName.ToLower())}";

    public List<string>? RacialAbilities { get; set; } 

    public List<ClassDto>? AllowedClasses { get; set; }
}
