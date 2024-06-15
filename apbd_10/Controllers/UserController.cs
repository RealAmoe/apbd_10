using apbd_10.Models;
using apbd_10.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apbd_10.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [AllowAnonymous]
    [HttpPost("/register")]
    public async Task<IActionResult> Register(LoginDto login)
    {
        await _userService.AddUserAsync(login);
        
        return NoContent();
    }
    
    [AllowAnonymous]
    [HttpPost("/login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        return Ok(await _userService.LoginUserAsync(loginDto));
    }
    
    [Authorize]
    [HttpPost("/refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenDto refreshTokenDto)
    {
        return Ok(await _userService.UpdateTokensAysnc(refreshTokenDto));
    }
}