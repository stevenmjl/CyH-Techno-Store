using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyH_Techno_Store.Models;
public class Productos
{
    [Key]
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "El nombre del Productos es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "El precio unitario es obligatorio")]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal PrecioUnitario { get; set; }

    [StringLength(256, ErrorMessage = "La descripción no puede exceder 256 caracteres")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
    public int Stock { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime FechaRegistro { get; set; }

    [StringLength(255, ErrorMessage = "La ruta de la imagen no puede exceder 255 caracteres")]
    public string? ImagenUrl { get; set; } 
    
    public virtual ICollection<DetalleFacturas> DetalleFactura { get; set; } = new List<DetalleFacturas>();

    public virtual ICollection<DetalleFacturaAdmins> DetalleFacturaAdmin { get; set; } = new List<DetalleFacturaAdmins>();

    [Required(ErrorMessage = "La categoría es obligatoria")]
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}