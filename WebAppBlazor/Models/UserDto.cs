﻿using System.ComponentModel.DataAnnotations;

namespace WebAppBlazor.Models;

public class UserDto
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Email { get; set; } = "";

    public string Password { get; set; } = "";

}
