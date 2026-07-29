using Inventory.Web.Models.Auth;
using Inventory.Web.Services.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inventory.Web.Controllers;
public class AuthController : Controller
{
    private readonly IAuthApiService _authApiService;
    public AuthController(IAuthApiService authApiService)
    {
        _authApiService = authApiService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(LoginDto request)
    {
        if (!ModelState.IsValid)
            return View("Login");

        var response = await _authApiService.Login(request);
        if (!response.Success)
        {
            foreach (var error in response.Errors!)
            {
                ModelState.AddModelError(string.Empty, response.Message!.Message);
            }
            return View(request);
        }
            
        var claims = new List<Claim>{new Claim(ClaimTypes.Name, request.Name)};
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var auth = new AuthenticationProperties();
        auth.StoreTokens(new[]
        {
            new AuthenticationToken
            {
                Name = "access_token",
                Value = response!.Data!.token.ToString()
            }
        });
        auth.IsPersistent = true;
        auth.ExpiresUtc = response.Data!.expiration;
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, auth);
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [HttpPost]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Auth");
    }
}
