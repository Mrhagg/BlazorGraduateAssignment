﻿namespace BlazorWebApi.Models;

public class Roles
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<Specialization> ClassSpecializations { get; set; }
}
