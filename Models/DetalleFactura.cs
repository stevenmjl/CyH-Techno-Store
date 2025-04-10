using CyH_Techno_Store.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyH_Techno_Store.Models;

public class DetalleFacturass
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DetalleId { get; set; }
    public int FacturasId { get; set; }

    [ForeignKey("FacturasId")]
    public virtual Facturass Facturas { get; set; }


    [Required(ErrorMessage = "El Productos es obligatorio")]
    public int ProductosId { get; set; }

    [ForeignKey("ProductosId")]
    public virtual Productoss Productos { get; set; }

    [Required(ErrorMessage = "La cantidad es obligatoria")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad mínima es 1")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "El precio unitario es obligatorio")]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal PrecioUnitario { get; set; }


    [Column(TypeName = "decimal(18,2)")]
    public decimal Subtotal => Cantidad * PrecioUnitario;
}