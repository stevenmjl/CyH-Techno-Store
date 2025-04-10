
using CyH_Techno_Store.Models;
using CyH_Techno_Store;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyH_Techno_Store.Models
{
    public class Facturass
    {
        [Key]
        public int FacturasId { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        // Relación corregida con UserAccounts
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }  // Nombre consistente

        [ForeignKey("UsuarioId")]
        public virtual UserAccounts Usuario { get; set; }  // Nombre más descriptivo

        public virtual ICollection<DetalleFacturass> Detalles { get; set; } = new List<DetalleFacturass>();
    }
}