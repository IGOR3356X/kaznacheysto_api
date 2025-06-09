using KaznacheystvoCalendar.DTO.Authorization;
using KaznacheystvoCalendar.Interfaces.ISevices;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace KaznacheystvoCalendar.Controllers;

public class AuthorizationController : Controller
{
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;
    
    public AuthorizationController(ITokenService tokenService, IUserService userService)
    {
        _tokenService = tokenService;
        _userService = userService;
    }
    
    [HttpPost("api/login")]
    public async Task<IActionResult> Login([FromBody]AuthDTO auth)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { message = "Минимаьная длинна логина/паролья 1 символ" });
        var user = await _userService.LoginUserAsync(auth);

        if (user == null)
        {
            return Unauthorized(new {message = "Пользователь не найден или введён неправильный пароль"});
        }

        return Ok(
            new ResponseAuthDTO()
            {
                Token = _tokenService.CreateToken(user)
            }
        );
    }
}