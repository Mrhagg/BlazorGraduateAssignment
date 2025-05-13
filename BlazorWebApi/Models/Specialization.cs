using System.Data;

namespace BlazorWebApi.Models;

public class Specialization
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public int WowClassId { get; set; }
    public WowClass WowClass { get; set; }

    public int RoleId { get; set; }
    public Roles Role { get; set; }

}