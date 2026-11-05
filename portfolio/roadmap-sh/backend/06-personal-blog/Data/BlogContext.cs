
using Microsoft.EntityFrameworkCore;
using PersonalBlog.Models;

namespace PersonalBlog.Data;

public class BlogContext :DbContext
{
    //Opciones para la conexión, dependientes del proveedor.
    //Se configuran en Program.cs
    public BlogContext(DbContextOptions<BlogContext> options) 
        : base(options)
    {
    }

    //Representation de una tabla basada en un modelo
    public DbSet<BlogPost> Posts {get; set;} = default!;
}