using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;
using PersonalBlog.Models;

namespace PersonalBlog.Controllers;

public class HomeController : Controller
{
    private readonly BlogContext _context;
    public HomeController(BlogContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<BlogPost> recentPosts = await _context.Posts
            .OrderByDescending(p => p.PublicationDate)
            .Take(3)
            .ToListAsync();
        return View(recentPosts);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
