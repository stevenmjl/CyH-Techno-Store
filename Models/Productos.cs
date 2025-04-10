
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyH_Techno_Store.Models;

public class Productoss
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductosId { get; set; }

    [Required(ErrorMessage = "El nombre del Productos es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; }

    [StringLength(256, ErrorMessage = "La descripción no puede exceder 256 caracteres")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
    public int Stock { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

   
    [StringLength(255, ErrorMessage = "La ruta de la imagen no puede exceder 255 caracteres")]
    public string ImagenUrl { get; set; } 
    
    public virtual ICollection<DetalleFacturass> DetallesFacturas { get; set; } = new List<DetalleFacturass>();

}