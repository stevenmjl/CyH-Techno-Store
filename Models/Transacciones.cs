using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyH_Techno_Store.Models;
public class Transacciones
{
    [Key]
    public int TransaccionId { get; set; }

    [Required(ErrorMessage = "Debe tener un monto válido.")]
    [Column(TypeName = "decimal(18,2)")]
    [Range(1, double.MaxValue, ErrorMessage = "Ingrese un monto mayor a 0.")]
    public decimal Monto { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime FechaRegistro { get; set; }

    [Required(ErrorMessage = "Debe agregar un tipo.")]
    [RegularExpression("^(Ingreso|Gasto)$")]
    public string? Tipo { get; set; }

    [ForeignKey("FacturaId")]
    [Required(ErrorMessage = "Debe agregar el ID de la factura.")]
    public int FacturaId { get; set; }

    public Facturas? Facturas { get; set; }
    public FacturaAdmins? FacturaAdmins { get; set; }
}