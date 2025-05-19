namespace BlazorWebApi.Models;

public class Factions
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ImageFileName { get; set; }
    public ICollection<CharacterRace> CharacterRaces { get; set; } = new List<CharacterRace>();
}
