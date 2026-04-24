using Microsoft.AspNetCore.Mvc;
using Zajezdnia.DTOs;
using Zajezdnia.Services;

namespace Zajezdnia.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        var result = await _authService.RegisterAsync(dto);
        if (!result)
        {
            ModelState.AddModelError("", "Login już istnieje");
            return View(dto);
        }

        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        var user = await _authService.LoginAsync(dto);
        if (user == null)
        {
            ModelState.AddModelError("", "Nieprawidłowy login lub hasło");
            return View(dto);
        }

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("UserUsername", user.Username);

        return RedirectToAction("Index", "Zajezdnie");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}