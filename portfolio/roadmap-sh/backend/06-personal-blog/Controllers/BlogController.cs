using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Controllers;

public class BlogController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}