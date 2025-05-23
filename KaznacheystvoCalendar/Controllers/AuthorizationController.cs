﻿using KaznacheystvoCalendar.DTO.Authorization;
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
            return BadRequest();
        var user = await _userService.LoginUserAsync(auth);

        if (user == null)
        {
            return Unauthorized(new {message = "Username not found and/or password"});
        }

        return Ok(
            new ResponseAuthDTO()
            {
                Token = _tokenService.CreateToken(user)
            }
        );
    }
}