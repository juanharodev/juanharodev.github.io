using Microsoft.EntityFrameworkCore;
using PersonalBlog.Data;

namespace PersonalBlog.Models;

public static class SeedBlogData
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        //Creamos un contexto basado en el proveedor de servicios
        using(BlogContext? context = new BlogContext(serviceProvider.GetRequiredService<DbContextOptions<BlogContext>>()))
        {
            //Ya hay datos
            if(context.Posts.Any())
            {
                return;
            }
            
            //Agregamos los valores
            context.Posts.AddRange(
                new BlogPost
                {
                    PostTitle = "Mi primera publicación",
                    PublicationDate = DateTime.Parse("2026-10-23"),
                    Content = "Hola mundo! Este es mi primera publicación!!"
                },
                new BlogPost
                {
                    PostTitle = "Una publicación de muchas",
                    PublicationDate = DateTime.Parse("2026-10-23"),
                    Content = "Hola este solo es una publicación de las muchas que se harán!!"
                },
                new BlogPost
                {
                    PostTitle = "Publicación de prueba",
                    PublicationDate = DateTime.Parse("2026-10-23"),
                    Content = "Solo probamos que el sistema funcione!"
                }
            );
            //Aplicamos los cambios hechos en la clase a la base de datos
            context.SaveChanges();
        }
    }
}