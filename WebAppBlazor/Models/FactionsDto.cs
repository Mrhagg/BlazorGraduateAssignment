namespace WebAppBlazor.Models;

public class FactionDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public List<CharacterRaceDto>? CharacterRaces { get; set; }
}
