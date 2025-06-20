﻿using System.ComponentModel.DataAnnotations;

namespace KaznacheystvoCalendar.DTO.User;

public class CreateUserDTO
{
    [MinLength(1)]
    public string Login { get; set; }
    [MinLength(1)]
    public string Password { get; set; } 
    
    public string FullName { get; set; }
    [Phone]
    public string Telephone { get; set; }
    [EmailAddress]
    public string Email { get; set; }

    public int? RoleId { get; set; }

    public int? DepartmentId { get; set; }
}