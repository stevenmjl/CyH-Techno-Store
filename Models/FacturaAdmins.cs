using System.ComponentModel.DataAnnotations;

namespace CyH_Techno_Store.Models;
public class FacturaAdmins
{
    [Key]
    public int FacturaAdminId { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime FechaRegistro { get; set; }

    [Required(ErrorMessage = "El usuario es obligatorio")]
    public int UsuarioId { get; set; }
    public Usuarios? Usuarios { get; set; }

    [Required(ErrorMessage = "La categoría es obligatoria")]
    public int ProveedorId { get; set; }
    public Proveedores? Proveedores { get; set; }

    public ICollection<DetalleFacturaAdmins> DetalleFacturaAdmin { get; set; } = new List<DetalleFacturaAdmins>();
}