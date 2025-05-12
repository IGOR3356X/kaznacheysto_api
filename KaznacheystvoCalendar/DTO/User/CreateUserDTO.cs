namespace KaznacheystvoCalendar.DTO.User;

public class CreateUserDTO
{
    public string Login { get; set; }

    public string Password { get; set; } 

    public string FullName { get; set; }

    public string Telephone { get; set; }
    
    public string? Photo { get; set; }

    public string Email { get; set; }

    public int? RoleId { get; set; }

    public int? DepartmentId { get; set; }
}