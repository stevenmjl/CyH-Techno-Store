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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración inicial de usuarios
        modelBuilder.Entity<Usuarios>().ToTable("Usuarios");
        modelBuilder.Entity<Usuarios>().HasData(
        new Usuarios
        {
             UsuarioId = 1,
             UserName = "AdminPedro",
             Password = "Pedro50...",
             Correo = "AdminPedro@gmail.com",
             NumeroTarjeta = "1234567890123456789",
             FechaRegistro = new DateTime(2024, 1, 1),
             Telefono = "8091234567",
             Direccion = "Calle Principal #1",
             Role = "Admin"
        },
        new Usuarios
        {
             UsuarioId = 2,
             UserName = "AdminJose",
             Password = "Josep777",
             Correo = "AdminJose@gmail.com",
             NumeroTarjeta = "9876543210987654321",
             FechaRegistro = new DateTime(2024, 1, 1),
             Telefono = "8092345678",
             Direccion = "Avenida Siempre Viva #2",
             Role = "Admin"
        },
        new Usuarios
        {
             UsuarioId = 3,
             UserName = "AdminSusan",
             Password = "GatitaLinda15",
             Correo = "AdminSusan@gmail.com",
             NumeroTarjeta = "1111222233334444555",
             FechaRegistro = new DateTime(2024, 1, 1),
             Telefono = "8093456789",
             Direccion = "Calle Luna #3",
             Role = "Admin"
        },
        new Usuarios
        {
             UsuarioId = 4,
             UserName = "AdminVannesa",
             Password = "Venaros60...",
             Correo = "AdminVannesa@gmail.com",
             NumeroTarjeta = "5555666677778888999",
             FechaRegistro = new DateTime(2024, 1, 1),
             Telefono = "8094567890",
             Direccion = "Calle Sol #4",
             Role = "Admin"
        },
        new Usuarios
        {
             UsuarioId = 5,
             UserName = "AdminJuan",
             Password = "Master...200M",
             Correo = "AdminJuan@gmail.com",
             NumeroTarjeta = "1122334455667788990",
             FechaRegistro = new DateTime(2024, 1, 1),
             Telefono = "8291234567",
             Direccion = "Calle 5 de Abril",
             Role = "Admin"
        },
        new Usuarios
        {
             UsuarioId = 6,
             UserName = "FloresJJJ",
             Password = "Mis12Flores",
             Correo = "FloresJJJ@gmail.com",
             NumeroTarjeta = "9988776655443322110",
             FechaRegistro = new DateTime(2025, 4, 1),
             Telefono = "8292345678",
             Direccion = "Avenida Las Palmas",
             Role = "User"
        },
        new Usuarios
        {
             UsuarioId = 7,
             UserName = "LoboAterrador",
             Password = "Lobos9999",
             Correo = "Tengo4Lobos@gmail.com",
             NumeroTarjeta = "1234432112344321123",
             FechaRegistro = new DateTime(2025, 4, 2),
             Telefono = "8493456789",
             Direccion = "Residencial Brisas del Este",
             Role = "User"
        },
        new Usuarios
        {
             UsuarioId = 8,
             UserName = "PlantasMuchas",
             Password = "van999Plantas",
             Correo = "Melon14@gmail.com",
             NumeroTarjeta = "3211233211233211234",
             FechaRegistro = new DateTime(2025, 4, 3),
             Telefono = "8494567890",
             Direccion = "Villa Mella, Santo Domingo",
             Role = "User"
        },
        new Usuarios
        {
             UsuarioId = 9,
             UserName = "ResposteraJulia",
             Password = "PastelDePapa20",
             Correo = "ResposteraJulia@gmail.com",
             NumeroTarjeta = "1231231231231231231",
             FechaRegistro = new DateTime(2025, 4, 4),
             Telefono = "8095678901",
             Direccion = "Ensanche La Fe",
             Role = "User"
        },
        new Usuarios
        {
             UsuarioId = 10,
             UserName = "Mariano111",
             Password = "VenusVSTierra",
             Correo = "MarVelous@gmail.com",
             NumeroTarjeta = "3213213213213213213",
             FechaRegistro = new DateTime(2025, 4, 5),
             Telefono = "8295678901",
             Direccion = "Zona Colonial",
             Role = "User"
        });


        modelBuilder.Entity<Categoria>().HasData(
        new Categoria
        {
            CategoriaId = 1,
            Nombre = "Teléfonos móviles",
            Descripcion = "Dispositivos electrónicos portátiles utilizados para comunicarse mediante llamadas o mensajes."
        },
        new Categoria
        {
            CategoriaId = 2,
            Nombre = "Computadoras portátiles",
            Descripcion = "Equipos electrónicos de procesamiento de datos diseñados para uso personal y profesional."
        },
        new Categoria
        {
            CategoriaId = 3,
            Nombre = "Tabletas electrónicas",
            Descripcion = "Dispositivos electrónicos con pantalla táctil utilizados para navegación, lectura y multimedia."
        },
        new Categoria
        {
            CategoriaId = 4,
            Nombre = "Televisores inteligentes",
            Descripcion = "Aparatos electrónicos para visualización de contenido audiovisual con funciones de conectividad."
        },

        new Categoria
        {
            CategoriaId = 5,
            Nombre = "Accesorios electrónicos",
            Descripcion = "Componentes electrónicos complementarios como cargadores, fundas y cables de conexión digital."
        });


        modelBuilder.Entity<Productos>().HasData(
        new Productos
        {
            ProductoId = 1,
            Nombre = "Samsung Galaxy S23",
            PrecioUnitario = 50940.99m,
            Descripcion = "Teléfono móvil con pantalla AMOLED y triple cámara de alta resolución.",
            Stock = 10,
            FechaRegistro = new DateTime(2024, 6, 1),
            ImagenUrl = "https://cdn1.smartprix.com/rx-izLSMVlI0-w1200-h1200/zLSMVlI0.jpg",
            CategoriaId = 1
        },
        new Productos
        {
            ProductoId = 2,
            Nombre = "iPhone 14 Pro",
            PrecioUnitario = 65940.00m,
            Descripcion = "Teléfono inteligente con tecnología Face ID y cámara de 48 MP.",
            Stock = 10,
            FechaRegistro = new DateTime(2024, 6, 1),
            ImagenUrl = "https://phonesdata.com/files/models/Apple-iPhone-14-Pro-907.jpg",
            CategoriaId = 1
        },
        new Productos
        {
            ProductoId = 3,
            Nombre = "Dell XPS 13",
            PrecioUnitario = 74970.00m,
            Descripcion = "Laptop ultradelgada con pantalla táctil y procesador Intel i7.",
            Stock = 10,
            FechaRegistro = new DateTime(2024, 6, 1),
            ImagenUrl = "https://i.pcmag.com/imagery/reviews/06Ug0e0tlPFOh5qZAAcpq10-1.fit_lim.size_1200x630.v1688849689.jpg",
            CategoriaId = 2
        },
        new Productos
        {
            ProductoId = 4,
            Nombre = "HP Pavilion 15",
            PrecioUnitario = 47999.40m,
            Descripcion = "Computadora portátil con buen rendimiento para el trabajo diario.",
            Stock = 10,
            FechaRegistro = new DateTime(2024, 6, 1),
            ImagenUrl = "https://vitinhthienan.vn/upload/hinhanh/hp-all/ban-phim-laptop-hp/keyboard-hp-15-ab030tu/hp-15-ab030tu.jpg",
            CategoriaId = 2
        },
        new Productos
        {
            ProductoId = 5,
            Nombre = "iPad Air 2022",
            PrecioUnitario = 35940.00m,
            Descripcion = "Tableta de alto rendimiento con chip M1 y pantalla de 10.9 pulgadas.",
            Stock = 10,
            FechaRegistro = new DateTime(2024, 6, 1),
            ImagenUrl = "https://cdn.arstechnica.net/wp-content/uploads/2022/03/2022-iPad-Air-front-on-800x546.jpeg",
            CategoriaId = 3
        },
        new Productos
        {
            ProductoId = 6,
            Nombre = "Samsung Galaxy Tab S8",
            PrecioUnitario = 38999.99m,
            Descripcion = "Tableta Android con gran rendimiento y pantalla AMOLED.",
            Stock = 10,
            FechaRegistro = new DateTime(2024, 6, 1),
            ImagenUrl = "https://www.zdnet.com/a/img/2022/07/17/fd8b66a7-f705-42d6-af93-08afa6c0965a/samsung-galaxy-tab-s8-ultra-8.jpg",
            CategoriaId = 3
        },
        new Productos
        {
            ProductoId = 7,
            Nombre = "LG OLED C2 55",
            PrecioUnitario = 83940.00m,
            Descripcion = "Televisor OLED con resolución 4K y compatibilidad con Dolby Vision.",
            Stock = 10,
            FechaRegistro = new DateTime(2024, 6, 1),
            ImagenUrl = "https://th.bing.com/th/id/OIP.LApG9RSTogWAqc7KM05JAgHaE6?rs=1&pid=ImgDetMain",
            CategoriaId = 4
        },
        new Productos
        {
            ProductoId = 8,
            Nombre = "Samsung QLED Q70",
            PrecioUnitario = 59999.40m,
            Descripcion = "Smart TV de 55 pulgadas con tecnología QLED y control por voz.",
            Stock = 10,
            FechaRegistro = new DateTime(2024, 6, 1),
            ImagenUrl = "https://th.bing.com/th/id/OIP.zYVsmU5Mk9SPgGfjTkmk4AHaE8?rs=1&pid=ImgDetMain",
            CategoriaId = 4
        },
        new Productos
        {
            ProductoId = 9,
            Nombre = "Canon EOS M50",
            PrecioUnitario = 41940.00m,
            Descripcion = "Cámara digital sin espejo ideal para fotografía profesional.",
            Stock = 10,
            FechaRegistro = new DateTime(2024, 6, 1),
            ImagenUrl = "https://www.bhphotovideo.com/images/images1000x1000/canon_4728c006_eos_m50_mark_ii_1598385.jpg",
            CategoriaId = 5
        },
        new Productos
        {
            ProductoId = 10,
            Nombre = "Sony Alpha a6400",
            PrecioUnitario = 53999.50m,
            Descripcion = "Cámara compacta de lentes intercambiables con enfoque automático rápido.",
            Stock = 10,
            FechaRegistro = new DateTime(2024, 6, 1),
            ImagenUrl = "https://i5.walmartimages.com/asr/49d86635-3a17-4550-a3ac-937b3bd546a7_1.007c196265cf588fec8a2911a6659373.jpeg",
            CategoriaId = 5
        });


        modelBuilder.Entity<Proveedores>().HasData(
        new Proveedores
        {
            ProveedorId = 1,
            Correo = "contacto@tecnomundo.com",
            RNC = "123456789",
            Nombres = "Tecno Mundo",
            FechaRegistro = new DateTime(2022, 3, 15),
            Telefono = "+18294321010"
        },
        new Proveedores
        {
            ProveedorId = 2,
            Correo = "ventas@electronicalopez.com",
            RNC = "987654321",
            Nombres = "Electronica López",
            FechaRegistro = new DateTime(2023, 1, 10),
            Telefono = "+18095551234"
        },
        new Proveedores
        {
            ProveedorId = 3,
            Correo = "proveedor@megabyte.com",
            RNC = "456789123",
            Nombres = "Mega Byte",
            FechaRegistro = new DateTime(2023, 6, 5),
            Telefono = "+18097894567"
        },
        new Proveedores
        {
            ProveedorId = 4,
            Correo = "info@electrodigital.com",
            RNC = "321654987",
            Nombres = "Electro Digital",
            FechaRegistro = new DateTime(2024, 2, 22),
            Telefono = "+18296471230"
        },
        new Proveedores
        {
            ProveedorId = 5,
            Correo = "servicio@conectadordr.com",
            RNC = "789123456",
            Nombres = "Conectador RD",
            FechaRegistro = new DateTime(2024, 7, 18),
            Telefono = "+18093001234"
        });


        modelBuilder.Entity<FacturaAdmins>().HasData(
            new FacturaAdmins { FacturaAdminId = 1, FechaRegistro = new DateTime(2024, 6, 1), UsuarioId = 1, ProveedorId = 1 },

            new FacturaAdmins { FacturaAdminId = 2, FechaRegistro = new DateTime(2024, 6, 1), UsuarioId = 2, ProveedorId = 2 },

            new FacturaAdmins { FacturaAdminId = 3, FechaRegistro = new DateTime(2024, 6, 1), UsuarioId = 3, ProveedorId = 3 },

            new FacturaAdmins { FacturaAdminId = 4, FechaRegistro = new DateTime(2024, 6, 1), UsuarioId = 4, ProveedorId = 4 },

            new FacturaAdmins { FacturaAdminId = 5, FechaRegistro = new DateTime(2024, 6, 1), UsuarioId = 5, ProveedorId = 5 }
        );

        modelBuilder.Entity<DetalleFacturaAdmins>().HasData(
            new DetalleFacturaAdmins { DetalleFacturaAdminId = 1, Cantidad = 15, PrecioUnitario = 50940.99m, FacturaAdminId = 1, ProductoId = 1 },
            new DetalleFacturaAdmins { DetalleFacturaAdminId = 2, Cantidad = 15, PrecioUnitario = 65940.00m, FacturaAdminId = 1, ProductoId = 2 },

            new DetalleFacturaAdmins { DetalleFacturaAdminId = 3, Cantidad = 15, PrecioUnitario = 74970.00m, FacturaAdminId = 2, ProductoId = 3 },
            new DetalleFacturaAdmins { DetalleFacturaAdminId = 4, Cantidad = 15, PrecioUnitario = 47999.40m, FacturaAdminId = 2, ProductoId = 4 },

            new DetalleFacturaAdmins { DetalleFacturaAdminId = 5, Cantidad = 15, PrecioUnitario = 35940.00m, FacturaAdminId = 3, ProductoId = 5 },
            new DetalleFacturaAdmins { DetalleFacturaAdminId = 6, Cantidad = 15, PrecioUnitario = 38999.99m, FacturaAdminId = 3, ProductoId = 6 },

            new DetalleFacturaAdmins { DetalleFacturaAdminId = 7, Cantidad = 15, PrecioUnitario = 83940.00m, FacturaAdminId = 4, ProductoId = 7 },
            new DetalleFacturaAdmins { DetalleFacturaAdminId = 8, Cantidad = 15, PrecioUnitario = 59999.40m, FacturaAdminId = 4, ProductoId = 8 },

            new DetalleFacturaAdmins { DetalleFacturaAdminId = 9, Cantidad = 15, PrecioUnitario = 41940.00m, FacturaAdminId = 5, ProductoId = 9 },
            new DetalleFacturaAdmins { DetalleFacturaAdminId = 10, Cantidad = 15, PrecioUnitario = 53999.50m, FacturaAdminId = 5, ProductoId = 10 }
        );

        modelBuilder.Entity<Facturas>().HasData(
            new Facturas { FacturaId = 1, FechaRegistro = new DateTime(2025, 4, 1), UsuarioId = 6 },

            new Facturas { FacturaId = 2, FechaRegistro = new DateTime(2025, 4, 2), UsuarioId = 7 },

            new Facturas { FacturaId = 3, FechaRegistro = new DateTime(2025, 4, 3), UsuarioId = 8 },

            new Facturas { FacturaId = 4, FechaRegistro = new DateTime(2025, 4, 4), UsuarioId = 9 },

            new Facturas { FacturaId = 5, FechaRegistro = new DateTime(2025, 4, 5), UsuarioId = 10 }
        );

        modelBuilder.Entity<DetalleFacturas>().HasData(
            new DetalleFacturas { DetalleFacturaId = 1, FacturaId = 1, ProductoId = 1, Cantidad = 5, PrecioUnitario = 50940.99m },
            new DetalleFacturas { DetalleFacturaId = 2, FacturaId = 1, ProductoId = 3, Cantidad = 5, PrecioUnitario = 74970.00m },

            new DetalleFacturas { DetalleFacturaId = 3, FacturaId = 2, ProductoId = 2, Cantidad = 5, PrecioUnitario = 65940.00m },
            new DetalleFacturas { DetalleFacturaId = 4, FacturaId = 2, ProductoId = 4, Cantidad = 5, PrecioUnitario = 47999.40m },

            new DetalleFacturas { DetalleFacturaId = 5, FacturaId = 3, ProductoId = 5, Cantidad = 5, PrecioUnitario = 35940.00m },
            new DetalleFacturas { DetalleFacturaId = 6, FacturaId = 3, ProductoId = 6, Cantidad = 5, PrecioUnitario = 38999.99m },

            new DetalleFacturas { DetalleFacturaId = 7, FacturaId = 4, ProductoId = 10, Cantidad = 5, PrecioUnitario = 53999.50m },
            new DetalleFacturas { DetalleFacturaId = 8, FacturaId = 4, ProductoId = 7, Cantidad = 5, PrecioUnitario = 83940.00m },

            new DetalleFacturas { DetalleFacturaId = 9, FacturaId = 5, ProductoId = 9, Cantidad = 5, PrecioUnitario = 41940.00m },
            new DetalleFacturas { DetalleFacturaId = 10, FacturaId = 5, ProductoId = 8, Cantidad = 5, PrecioUnitario = 59999.40m }
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

        // Configuración de nombres de tablas
        modelBuilder.Entity<DetalleFacturas>().ToTable("DetalleFacturas");
        modelBuilder.Entity<DetalleFacturaAdmins>().ToTable("DetalleFacturaAdmins");
    }
}