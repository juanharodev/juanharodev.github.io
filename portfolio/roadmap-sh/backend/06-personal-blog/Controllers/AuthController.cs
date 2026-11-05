using Microsoft.AspNetCore.Mvc;


namespace PersonalBlog.Controllers;

public class AuthController : Controller
{

    public IActionResult LogIn()
    {
        return View();
    }

    public IActionResult Forbidden()
    {
        return View();
    }
}