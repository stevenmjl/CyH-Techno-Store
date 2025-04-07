using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyH_Techno_Store.Models;
public class DetalleFacturaAdmins
{
    [Key]
    public int DetalleFacturaAdminId { get; set; }

    [Required(ErrorMessage = "Debe agregar una cantidad")]
    [Range(1, int.MaxValue)]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "El precio unitario es obligatorio")]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal PrecioUnitario { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Subtotal => Cantidad * PrecioUnitario;

    [Required(ErrorMessage = "Debe agregar el ID de la factura")]
    public int FacturaAdminId { get; set; }
    public FacturaAdmins? FacturaAdmins { get; set; }

    [Required(ErrorMessage = "Debe agregar un el ID del producto")]
    public int ProductoId { get; set; }
    public Productos? Productos { get; set; }
}