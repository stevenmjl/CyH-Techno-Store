using System.ComponentModel.DataAnnotations;

namespace CyH_Techno_Store.Models;

public class UserAccount
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Debe agregar el nombre de usuario.")]
    [MaxLength(100, ErrorMessage = "No puede ser mayor a 100 letras.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Debe agregar la contraseña.")]
    [MaxLength(100, ErrorMessage = "No puede ser mayor a 100 letras.")]
    public string? Password { get; set; }

    [MaxLength(5)]
    public string? Role { get; set; }
}