using Microsoft.EntityFrameworkCore;
using CyH_Techno_Store.Models;

namespace CyH_Techno_Store.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    // 1. Usa nombres pluralizados consistentemente para DbSet
    public DbSet<UserAccounts> UserAccountss { get; set; } // Cambiado a plural
    public DbSet<Productoss> Productoss { get; set; } // Asumiendo que el modelo se llama Productos
    public DbSet<Facturass> Facturass { get; set; } // Asumiendo que el modelo se llama Facturas
    public DbSet<DetalleFacturass> DetallesFacturas { get; set; } // Cambiado a plural

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 2. Configuración correcta del nombre de tabla y relaciones
        modelBuilder.Entity<UserAccounts>().ToTable("UserAccountss"); // Nombre explícito de tabla

        // 3. Configuración de HasData correcta
        modelBuilder.Entity<UserAccounts>().HasData(
            new UserAccounts
            {
                Id = 1,
                UserName = "Administrador",
                Password = "Qwe123...",
                NumeroTarjeta = "1234567891456978524",
                FechaRegistro = new DateTime(2020, 3, 4),
                Telefono = "8094627895",
                Direccion = "calle 18",
                Role = "Admin"
            },
            new UserAccounts
            {
                Id = 2,
                UserName = "Usuario",
                Password = "Asd123...",
                NumeroTarjeta = "999999999999999999",
                FechaRegistro = new DateTime(2025, 2, 1),
                Telefono = "8094627111",
                Direccion = "calle 890",
                Role = "User"
            }
        );

      
        modelBuilder.Entity<UserAccounts>()
       .HasMany(u => u.Facturass)
       .WithOne(f => f.Usuario)
       .HasForeignKey(f => f.UsuarioId);

        modelBuilder.Entity<Facturass>()
        .HasMany(f => f.Detalles)
        .WithOne(d => d.Facturas)
        .HasForeignKey(d => d.FacturasId)
        .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}