namespace BlazorWebApi.Models;

public class WowClass
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;

    public ICollection<RaceWowClass> RaceWowClasses { get; set; } = new List<RaceWowClass>();
    public ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();

}
