using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyH_Techno_Store.Models;

public class UserAccounts
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

    [StringLength(19, ErrorMessage = "El número de tarjeta no puede exceder 19 caracteres")]
    [Display(Name = "Número de Tarjeta")]
    public string? NumeroTarjeta { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime FechaRegistro { get; set; }

    [StringLength(25, MinimumLength = 8, ErrorMessage = "El teléfono debe tener entre 8 y 25 dígitos")]
    public string? Telefono { get; set; }

    [StringLength(256, MinimumLength = 6, ErrorMessage = "La dirección debe tener entre 6 y 256 caracteres")]
    public string? Direccion { get; set; }


    public virtual ICollection<Facturass> Facturass { get; set; } = new List<Facturass>();
}
