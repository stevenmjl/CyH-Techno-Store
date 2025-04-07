using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyH_Techno_Store.Models;
public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "Debe ingresar el nombre de la categoría.")]
    [RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$",
    ErrorMessage = "El nombre no debe contener números ni caracteres especiales.")]
    [StringLength(50, MinimumLength = 6,
    ErrorMessage = "El nombre debe tener entre 6 y 50 caracteres.")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "Debe ingresar la descripción de la categoría.")]
    [RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$",
    ErrorMessage = "La descripción no debe contener números ni caracteres especiales.")]
    [StringLength(256, MinimumLength = 10,
    ErrorMessage = "La descripción debe tener entre 256 y 10 caracteres.")]
    public string? Descripcion { get; set; }

    [ForeignKey("CategoriaId")]
    public ICollection<Productos> Productos { get; set; } = new List<Productos>();
}