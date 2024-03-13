using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using test_sample.DTO;
using test_sample.Services;

namespace test_sample.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
  private readonly IAuthService _authService;

  public AuthController(IAuthService authService)
  {
    _authService = authService;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromForm]RegisterDto dto)
  {
    if (dto.Password != dto.PasswordConfirm) return Unauthorized("Passwords donot match!.");
    var (success, user) = await _authService.Register(dto);
    if (!success) return Unauthorized("Email already registered");
    return Ok(user);
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromForm]LoginDto dto)
  {
    var result = await _authService.Login(dto);
    if (!result.Success) return Unauthorized("Invalid login credentials!.");
    var user = result.user!;

    List<Claim> claims = [
      new Claim(ClaimTypes.Name, user.Email),
      new Claim(ClaimTypes.Actor, user.Id.ToString())
    ];
    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    await HttpContext.SignInAsync(claimsIdentity.AuthenticationType, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties());
    return Ok(user);
  }

  [HttpPost("logout")]
  public async Task<IActionResult> Logout()
  {
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, new AuthenticationProperties());
    return Ok("Logged out successfully");
  }
}
