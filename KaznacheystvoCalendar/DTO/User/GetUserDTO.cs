﻿namespace KaznacheystvoCalendar.DTO.User;

public class GetUserDTO
{
    public string Login { get; set; }

    public string Password { get; set; } 

    public string FullName { get; set; }

    public string Telephone { get; set; }
    
    public string? Photo { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }

    public string Department { get; set; }
}