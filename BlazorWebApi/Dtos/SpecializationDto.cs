namespace BlazorWebApi.Dtos;

public class SpecializationDto
{
    public int Id { get; set; }

    public int RoleId { get; set; }
    public string? Name { get; set; }
    public string? RoleName { get; set; }
    public string? DamageType { get; set; }

    public bool IsBeginnerFriendly { get; set; }
}
