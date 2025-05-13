namespace BlazorWebApi.Dtos;

public class RaceDto
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<WowClassDto> AllowedClasses { get; set; }
}
