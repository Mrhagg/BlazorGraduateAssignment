namespace WebAppBlazor.Models;

public class FactionDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImageFileName { get; set; }
    public string ImageUrl => $"images/logos/{(string.IsNullOrEmpty(ImageFileName) ? Name!.Replace(" ", "").ToLower() + ".jpg" : ImageFileName.ToLower())}";
    public List<CharacterRaceDto>? CharacterRaces { get; set; }
}
