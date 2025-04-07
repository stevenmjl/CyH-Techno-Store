using System.ComponentModel.DataAnnotations;

namespace CyH_Techno_Store.Models;
public class LoginViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor agregar un nombre de usuario.")]
    public string? UserName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Por favor agregar una contraseña.")]
    public string? Password { get; set; }
}