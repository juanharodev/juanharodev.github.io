using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;

namespace PersonalBlog.Controllers;

public class BlogController : Controller
{
    private readonly BlogContext _context;
    public BlogController(BlogContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        return View(await _context.Posts.ToListAsync());
    }
}