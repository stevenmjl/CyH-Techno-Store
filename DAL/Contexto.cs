using Microsoft.EntityFrameworkCore;
using CyH_Techno_Store.Models;

namespace CyH_Techno_Store.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Usuarios> Usuarios { get; set; }
    public DbSet<Productos> Productos { get; set; }
    public DbSet<Facturas> Facturas { get; set; }
    public DbSet<DetalleFacturas> DetalleFacturas { get; set; }
    public DbSet<FacturaAdmins> FacturaAdmins { get; set; }
    public DbSet<DetalleFacturaAdmins> DetalleFacturaAdmins { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Proveedores> Proveedores { get; set; }
    public DbSet<Transacciones> Transacciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración inicial de usuarios
        modelBuilder.Entity<Usuarios>().ToTable("Usuarios");
        modelBuilder.Entity<Usuarios>().HasData(
            new Usuarios
            {
                UsuarioId = 1,
                UserName = "Administrador",
                Password = "Qwe123...",
                Correo = "Admin@gmail.com",
                NumeroTarjeta = "1234567891456978524",
                FechaRegistro = new DateTime(2020, 3, 4),
                Telefono = "8094627895",
                Direccion = "calle 18",
                Role = "Admin"
            },
            new Usuarios
            {
                UsuarioId = 2,
                UserName = "Usuario",
                Password = "Asd123...",
                Correo = "User@gmail.com",
                NumeroTarjeta = "999999999999999999",
                FechaRegistro = new DateTime(2025, 2, 1),
                Telefono = "8094627111",
                Direccion = "calle 890",
                Role = "User"
            }
        );

        // Relaciones de Usuarios
        modelBuilder.Entity<Usuarios>()
            .HasMany(u => u.Facturas)
            .WithOne(f => f.Usuarios)
            .HasForeignKey(f => f.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Usuarios>()
            .HasMany(u => u.FacturasAdmin)
            .WithOne(f => f.Usuarios)
            .HasForeignKey(f => f.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relaciones de Facturas
        modelBuilder.Entity<Facturas>()
            .HasMany(f => f.DetalleFacturas)
            .WithOne(d => d.Facturas)
            .HasForeignKey(d => d.FacturaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relaciones de FacturaAdmins
        modelBuilder.Entity<FacturaAdmins>()
            .HasMany(f => f.DetalleFacturaAdmin)
            .WithOne(d => d.FacturaAdmins)
            .HasForeignKey(d => d.FacturaAdminId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FacturaAdmins>()
            .HasOne(f => f.Proveedores)
            .WithMany(p => p.FacturaAdmin)
            .HasForeignKey(f => f.ProveedorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relaciones de Productos
        modelBuilder.Entity<Productos>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Productos>()
            .HasMany(p => p.DetalleFactura)
            .WithOne(d => d.Productos)
            .HasForeignKey(d => d.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Productos>()
            .HasMany(p => p.DetalleFacturaAdmin)
            .WithOne(d => d.Productos)
            .HasForeignKey(d => d.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relaciones de Transacciones
        modelBuilder.Entity<Transacciones>()
            .HasOne(t => t.Facturas)
            .WithMany()
            .HasForeignKey(t => t.FacturaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transacciones>()
            .HasOne(t => t.FacturaAdmins)
            .WithMany()
            .HasForeignKey(t => t.FacturaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuración de nombres de tablas
        modelBuilder.Entity<DetalleFacturas>().ToTable("DetalleFacturas");
        modelBuilder.Entity<DetalleFacturaAdmins>().ToTable("DetalleFacturaAdmins");
        modelBuilder.Entity<Transacciones>().ToTable("Transacciones");
    }
}