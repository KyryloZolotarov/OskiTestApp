using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Web.Server.Models.Requests;
using Web.Server.Services.Interfaces;
using Web.Server.ViewModels;

namespace Web.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var user = await _userService.LoginAsync(model);

        if (model.Email == user.Email && model.Password == user.Password)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Sid, user.Id),
                new(ClaimTypes.Name, user.FirstName),
                new(ClaimTypes.Surname, user.LastName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);

            return Ok();
        }

        return Unauthorized();
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(AddUserRequest model)
    {
        var user = await _userService.SignUpAsync(model);
        if (string.IsNullOrEmpty(user.Id)) return Ok();
        return BadRequest();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("CookieAuth");
        return Ok();
    }
}