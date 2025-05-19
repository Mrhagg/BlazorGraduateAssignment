namespace BlazorWebApi.Models;

public class CharacterRace
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? ImageFileName { get; set; }
    public int FactionId { get; set; }
    public Factions Faction { get; set; } = null!;

    public ICollection<RaceWowClass> RaceWowClasses { get; set; } = new List<RaceWowClass>();
}
