namespace KaznacheystvoCalendar.DTO.User;

public class GetAllUserDTO
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Email { get; set; } = null!;
    
    public int? RoleId { get; set; }

    public int? DepartmentId { get; set; }

}