using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;
using PersonalBlog.Models;

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

    public async Task<IActionResult> Post(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        BlogPost? post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if(post == null)
        {
            return NotFound();
        }
        
        return View(post);
    }
}