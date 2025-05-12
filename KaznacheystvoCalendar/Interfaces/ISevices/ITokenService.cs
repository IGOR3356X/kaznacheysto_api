using KaznacheystvoCalendar.Models;

namespace KaznacheystvoCalendar.Interfaces.ISevices;

public interface ITokenService
{
    string CreateToken(User? user);
}