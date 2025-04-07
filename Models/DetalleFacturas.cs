using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyH_Techno_Store.Models;
public class DetalleFacturas
{
    [Key]
    public int DetalleFacturaId { get; set; }

    [Required(ErrorMessage = "Debe agregar el ID de la factura")]
    public int FacturaId { get; set; }
    public Facturas? Facturas { get; set; }

    [Required(ErrorMessage = "El Productos es obligatorio")]
    public int ProductoId { get; set; }
    public Productos? Productos { get; set; }

    [Required(ErrorMessage = "La cantidad es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad mínima es 1")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "El precio unitario es obligatorio")]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal PrecioUnitario { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Subtotal { get; set; }
}