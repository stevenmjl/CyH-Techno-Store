using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CyHTechnoStore.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Correo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    RNC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.ProveedorId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Correo = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NumeroTarjeta = table.Column<string>(type: "nvarchar(19)", maxLength: 19, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoId);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacturaAdmins",
                columns: table => new
                {
                    FacturaAdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaAdmins", x => x.FacturaAdminId);
                    table.ForeignKey(
                        name: "FK_FacturaAdmins_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FacturaAdmins_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    FacturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.FacturaId);
                    table.ForeignKey(
                        name: "FK_Facturas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleFacturaAdmins",
                columns: table => new
                {
                    DetalleFacturaAdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FacturaAdminId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleFacturaAdmins", x => x.DetalleFacturaAdminId);
                    table.ForeignKey(
                        name: "FK_DetalleFacturaAdmins_FacturaAdmins_FacturaAdminId",
                        column: x => x.FacturaAdminId,
                        principalTable: "FacturaAdmins",
                        principalColumn: "FacturaAdminId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleFacturaAdmins_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleFacturas",
                columns: table => new
                {
                    DetalleFacturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleFacturas", x => x.DetalleFacturaId);
                    table.ForeignKey(
                        name: "FK_DetalleFacturas_Facturas_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Facturas",
                        principalColumn: "FacturaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleFacturas_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Dispositivos electrónicos portátiles utilizados para comunicarse mediante llamadas o mensajes.", "Teléfonos móviles" },
                    { 2, "Equipos electrónicos de procesamiento de datos diseñados para uso personal y profesional.", "Computadoras portátiles" },
                    { 3, "Dispositivos electrónicos con pantalla táctil utilizados para navegación, lectura y multimedia.", "Tabletas electrónicas" },
                    { 4, "Aparatos electrónicos para visualización de contenido audiovisual con funciones de conectividad.", "Televisores inteligentes" },
                    { 5, "Componentes electrónicos complementarios como cargadores, fundas y cables de conexión digital.", "Accesorios electrónicos" }
                });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "ProveedorId", "Correo", "FechaRegistro", "Nombres", "RNC", "Telefono" },
                values: new object[,]
                {
                    { 1, "contacto@tecnomundo.com", new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tecno Mundo", "123456789", "+18294321010" },
                    { 2, "ventas@electronicalopez.com", new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electronica López", "987654321", "+18095551234" },
                    { 3, "proveedor@megabyte.com", new DateTime(2023, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mega Byte", "456789123", "+18097894567" },
                    { 4, "info@electrodigital.com", new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electro Digital", "321654987", "+18296471230" },
                    { 5, "servicio@conectadordr.com", new DateTime(2024, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Conectador RD", "789123456", "+18093001234" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Correo", "Direccion", "FechaRegistro", "NumeroTarjeta", "Password", "Role", "Telefono", "UserName" },
                values: new object[,]
                {
                    { 1, "AdminPedro@gmail.com", "Calle Principal #1", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1234567890123456789", "Pedro50...", "Admin", "8091234567", "AdminPedro" },
                    { 2, "AdminJose@gmail.com", "Avenida Siempre Viva #2", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "9876543210987654321", "Josep777", "Admin", "8092345678", "AdminJose" },
                    { 3, "AdminSusan@gmail.com", "Calle Luna #3", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1111222233334444555", "GatitaLinda15", "Admin", "8093456789", "AdminSusan" },
                    { 4, "AdminVannesa@gmail.com", "Calle Sol #4", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5555666677778888999", "Venaros60...", "Admin", "8094567890", "AdminVannesa" },
                    { 5, "AdminJuan@gmail.com", "Calle 5 de Abril", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1122334455667788990", "Master...200M", "Admin", "8291234567", "AdminJuan" },
                    { 6, "FloresJJJ@gmail.com", "Avenida Las Palmas", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "9988776655443322110", "Mis12Flores", "User", "8292345678", "FloresJJJ" },
                    { 7, "Tengo4Lobos@gmail.com", "Residencial Brisas del Este", new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "1234432112344321123", "Lobos9999", "User", "8493456789", "LoboAterrador" },
                    { 8, "Melon14@gmail.com", "Villa Mella, Santo Domingo", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "3211233211233211234", "van999Plantas", "User", "8494567890", "PlantasMuchas" },
                    { 9, "ResposteraJulia@gmail.com", "Ensanche La Fe", new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "1231231231231231231", "PastelDePapa20", "User", "8095678901", "ResposteraJulia" },
                    { 10, "MarVelous@gmail.com", "Zona Colonial", new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "3213213213213213213", "VenusVSTierra", "User", "8295678901", "Mariano111" }
                });

            migrationBuilder.InsertData(
                table: "FacturaAdmins",
                columns: new[] { "FacturaAdminId", "FechaRegistro", "ProveedorId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2 },
                    { 3, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3 },
                    { 4, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4 },
                    { 5, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Facturas",
                columns: new[] { "FacturaId", "FechaRegistro", "UsuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 2, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 3, new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 4, new DateTime(2025, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 5, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "ProductoId", "CategoriaId", "Descripcion", "FechaRegistro", "ImagenUrl", "Nombre", "PrecioUnitario", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "Teléfono móvil con pantalla AMOLED y triple cámara de alta resolución.", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://cdn1.smartprix.com/rx-izLSMVlI0-w1200-h1200/zLSMVlI0.jpg", "Samsung Galaxy S23", 50940.99m, 10 },
                    { 2, 1, "Teléfono inteligente con tecnología Face ID y cámara de 48 MP.", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://phonesdata.com/files/models/Apple-iPhone-14-Pro-907.jpg", "iPhone 14 Pro", 65940.00m, 10 },
                    { 3, 2, "Laptop ultradelgada con pantalla táctil y procesador Intel i7.", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i.pcmag.com/imagery/reviews/06Ug0e0tlPFOh5qZAAcpq10-1.fit_lim.size_1200x630.v1688849689.jpg", "Dell XPS 13", 74970.00m, 10 },
                    { 4, 2, "Computadora portátil con buen rendimiento para el trabajo diario.", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://vitinhthienan.vn/upload/hinhanh/hp-all/ban-phim-laptop-hp/keyboard-hp-15-ab030tu/hp-15-ab030tu.jpg", "HP Pavilion 15", 47999.40m, 10 },
                    { 5, 3, "Tableta de alto rendimiento con chip M1 y pantalla de 10.9 pulgadas.", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://cdn.arstechnica.net/wp-content/uploads/2022/03/2022-iPad-Air-front-on-800x546.jpeg", "iPad Air 2022", 35940.00m, 10 },
                    { 6, 3, "Tableta Android con gran rendimiento y pantalla AMOLED.", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.zdnet.com/a/img/2022/07/17/fd8b66a7-f705-42d6-af93-08afa6c0965a/samsung-galaxy-tab-s8-ultra-8.jpg", "Samsung Galaxy Tab S8", 38999.99m, 10 },
                    { 7, 4, "Televisor OLED con resolución 4K y compatibilidad con Dolby Vision.", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://th.bing.com/th/id/OIP.LApG9RSTogWAqc7KM05JAgHaE6?rs=1&pid=ImgDetMain", "LG OLED C2 55", 83940.00m, 10 },
                    { 8, 4, "Smart TV de 55 pulgadas con tecnología QLED y control por voz.", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://th.bing.com/th/id/OIP.zYVsmU5Mk9SPgGfjTkmk4AHaE8?rs=1&pid=ImgDetMain", "Samsung QLED Q70", 59999.40m, 10 },
                    { 9, 5, "Cámara digital sin espejo ideal para fotografía profesional.", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://www.bhphotovideo.com/images/images1000x1000/canon_4728c006_eos_m50_mark_ii_1598385.jpg", "Canon EOS M50", 41940.00m, 10 },
                    { 10, 5, "Cámara compacta de lentes intercambiables con enfoque automático rápido.", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://i5.walmartimages.com/asr/49d86635-3a17-4550-a3ac-937b3bd546a7_1.007c196265cf588fec8a2911a6659373.jpeg", "Sony Alpha a6400", 53999.50m, 10 }
                });

            migrationBuilder.InsertData(
                table: "DetalleFacturaAdmins",
                columns: new[] { "DetalleFacturaAdminId", "Cantidad", "FacturaAdminId", "PrecioUnitario", "ProductoId" },
                values: new object[,]
                {
                    { 1, 15, 1, 50940.99m, 1 },
                    { 2, 15, 1, 65940.00m, 2 },
                    { 3, 15, 2, 74970.00m, 3 },
                    { 4, 15, 2, 47999.40m, 4 },
                    { 5, 15, 3, 35940.00m, 5 },
                    { 6, 15, 3, 38999.99m, 6 },
                    { 7, 15, 4, 83940.00m, 7 },
                    { 8, 15, 4, 59999.40m, 8 },
                    { 9, 15, 5, 41940.00m, 9 },
                    { 10, 15, 5, 53999.50m, 10 }
                });

            migrationBuilder.InsertData(
                table: "DetalleFacturas",
                columns: new[] { "DetalleFacturaId", "Cantidad", "FacturaId", "PrecioUnitario", "ProductoId" },
                values: new object[,]
                {
                    { 1, 5, 1, 50940.99m, 1 },
                    { 2, 5, 1, 74970.00m, 3 },
                    { 3, 5, 2, 65940.00m, 2 },
                    { 4, 5, 2, 47999.40m, 4 },
                    { 5, 5, 3, 35940.00m, 5 },
                    { 6, 5, 3, 38999.99m, 6 },
                    { 7, 5, 4, 53999.50m, 10 },
                    { 8, 5, 4, 83940.00m, 7 },
                    { 9, 5, 5, 41940.00m, 9 },
                    { 10, 5, 5, 59999.40m, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFacturaAdmins_FacturaAdminId",
                table: "DetalleFacturaAdmins",
                column: "FacturaAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFacturaAdmins_ProductoId",
                table: "DetalleFacturaAdmins",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFacturas_FacturaId",
                table: "DetalleFacturas",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFacturas_ProductoId",
                table: "DetalleFacturas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaAdmins_ProveedorId",
                table: "FacturaAdmins",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaAdmins_UsuarioId",
                table: "FacturaAdmins",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_UsuarioId",
                table: "Facturas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleFacturaAdmins");

            migrationBuilder.DropTable(
                name: "DetalleFacturas");

            migrationBuilder.DropTable(
                name: "FacturaAdmins");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
