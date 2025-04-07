using System.ComponentModel.DataAnnotations;

namespace CyH_Techno_Store.Models;
public class Transaccion
{
    [Key]
    public int TransaccionId { get; set; }

    [Required(ErrorMessage = "Debe tener un monto válido.")]
    [Range(1, double.MaxValue, ErrorMessage = "Ingrese un monto mayor a 0.")]
    public double Monto { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime FechaRegistro { get; set; }

    [Required(ErrorMessage = "Debe agregar un tipo.")]
    [RegularExpression("^(Ingreso|Gasto)$")]
    public string? Tipo { get; set; }

    [Required(ErrorMessage = "Debe agregar el ID de la factura.")]
    public int FacturaId { get; set; }
}