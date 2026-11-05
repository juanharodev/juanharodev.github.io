using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Data;
using PersonalBlog.Models;
using PersonalBlog.Services;


namespace PersonalBlog.Controllers;

public class AuthController : Controller
{

    public IActionResult LogIn()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogIn([Bind("Email,Password,IsPersistent")]UserLogin userLogin)
    {
        if (!ModelState.IsValid)
        {
            return View(userLogin);
        }
        
        var loggedUser = BasicAuthenticationService.AuthenticateUser(userLogin);

        if(loggedUser == null)
        {
            ModelState.AddModelError(string.Empty,"Error al iniciar sesión: Usuario o contraseña incorrectos");

            return View(userLogin);
        }

        ClaimsIdentity identity = new ClaimsIdentity(loggedUser.Claims,Constants.COOKIE_BASIC_SCHEME);
        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

        AuthenticationProperties props = new AuthenticationProperties
        {
            IsPersistent = userLogin.IsPersistent,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
        };

        await HttpContext.SignInAsync(Constants.COOKIE_BASIC_SCHEME, principal, props);

        string? returnUrl = HttpContext.Request.Query["returnUrl"];
        if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        
        return RedirectToAction("Profile","Auth");
    }

    [HttpPost]
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(Constants.COOKIE_BASIC_SCHEME);

        string? returnUrl = HttpContext.Request.Query["returnUrl"];
        if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            Redirect(returnUrl);
        }

        return RedirectToAction("Index","Home");
    }

    [Authorize]
    public IActionResult Profile()
    {
        return View();
    }

    public IActionResult Forbidden()
    {
        return View();
    }
}