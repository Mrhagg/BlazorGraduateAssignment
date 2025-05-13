namespace BlazorWebApi.Models;

public class RaceWowClass
{
    public int CharacterRaceId { get; set; }
    public CharacterRace CharacterRace { get; set; } = null!;

    public int WowClassId { get; set; }
    public WowClass WowClass { get; set; } = null!;
}