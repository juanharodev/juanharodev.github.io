using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;
using PersonalBlog.Models;

namespace PersonalBlog.Controllers;

[Authorize("Admin")]
public class AdminController : Controller
{
    private readonly BlogContext _context;

    public AdminController(BlogContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Posts.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PostTitle,PublicationDate,Content")] BlogPost blogPost)
    {
        if (!ModelState.IsValid)
        {
            return View(blogPost);
        }

        _context.Add(blogPost);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int? id)
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, [Bind("Id,PostTitle,PublicationDate,Content")] BlogPost post)
    {
        if(id != post.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(post);
        }

        try
        {
            _context.Update(post);
           await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PostExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

       return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }

        BlogPost? post = await _context.Posts.FirstOrDefaultAsync(p => p.Id ==id);

        if(post == null)
        {
            return NotFound();
        }

        return View(post);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmDelete(int id)
    {
        BlogPost? post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        if(post != null)
        {
            _context.Posts.Remove(post);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    bool PostExists(int id)
    {
        return _context.Posts.Any(p => p.Id == id);
    }
} 