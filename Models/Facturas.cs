using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyH_Techno_Store.Models;
public class Facturas
{
    [Key]
    public int FacturaId { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime FechaRegistro { get; set; }

    [Required(ErrorMessage = "El usuario es obligatorio")]
    public int UsuarioId { get; set; } 

    [ForeignKey("UsuarioId")]
    public Usuarios? Usuarios { get; set; }

    public ICollection<DetalleFacturas> DetalleFacturas { get; set; } = new List<DetalleFacturas>();
}