using System.ComponentModel.DataAnnotations;

namespace KaznacheystvoCalendar.DTO.Authorization;

public class AuthDTO
{
    [MinLength(1)]
    public string Login { get; set; }
    [MinLength(1)]
    public string Password { get; set; }
}