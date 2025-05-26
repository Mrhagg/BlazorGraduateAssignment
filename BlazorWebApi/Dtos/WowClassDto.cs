namespace BlazorWebApi.Dtos;

public class WowClassDto
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? RoleName { get; set; }


    public string? ImageFileName { get; set; }

    public bool IsBeginnerFriendly { get; set; }


    public List<string> Races { get; set; } = new List<string>();
    public List<SpecializationDto> Specializations { get; set; } = new List<SpecializationDto>();
}