using System.ComponentModel.DataAnnotations;

namespace CyH_Techno_Store.Models;

public class Proveedores
{
    [Key]
    public int ProveedorId { get; set; }

    [Required(ErrorMessage = "Debe ingresar un correo.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
    [StringLength(256, MinimumLength = 6)]
    public string? Correo { get; set; }

    [Required(ErrorMessage = "El RNC es obligatorio.")]
    [RegularExpression(@"^\d{9}$",
    ErrorMessage = "El RNC debe tener exactamente 9 dígitos numéricos.")]
    public string? RNC { get; set; }

    [Required(ErrorMessage = "Debe ingresar el nombre del proveedor.")]
    [RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$",
    ErrorMessage = "El nombre no debe contener números ni caracteres especiales.")]
    [StringLength(50, MinimumLength = 6,
    ErrorMessage = "El nombre debe tener entre 6 y 50 caracteres.")]
    public string? Nombre { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime FechaRegistro { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [RegularExpression(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$",
    ErrorMessage = "Formato internacional: +123456789")]
    [StringLength(15, MinimumLength = 8)]
    public string? Telefono { get; set; }
}