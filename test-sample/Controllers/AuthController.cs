using Microsoft.AspNetCore.Mvc;

namespace test_sample;

public class AuthController : Controller
{
    public IActionResult Login(string? returnUrl = "/") 
    {
      return View();
    }
    public IActionResult Register() 
    {
      return View();
    }
}
