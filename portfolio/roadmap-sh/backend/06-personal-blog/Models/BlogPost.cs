using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models;

public class BlogPost
{
    public int Id {get; set;}
    [DisplayName("Título")]
    public string? PostTitle{get; set;}
    [DataType(DataType.Date)]
    [DisplayName("Fecha de publicación")]
    public DateTime PublicationDate {get; set;}
    [DisplayName("Contenido")]
    public string? Content {get; set;}
}