using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PersonalBlog.Models;

public class UserData
{
    public string Username {get; set;} = null!;
    public string Email {get; set;} = null!;
    public string Password {get; set;} = null!;
    public List<Claim> Claims {get; set;} = null!;
}

public class UserLogin
{
    [DisplayName("Correo")]
    [Required(ErrorMessage ="Ingrese su correo")]
    public string Email {get; set;} = null!;
    [DisplayName("Contraseña")]
    [Required(ErrorMessage ="Ingrese su contraseña")]
    public string Password {get; set;} = null!;
    [DisplayName("Recuérdame")]
    public bool IsPersistent {get; set;} = false;
}