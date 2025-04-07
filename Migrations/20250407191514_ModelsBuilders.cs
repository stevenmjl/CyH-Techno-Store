using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CyHTechnoStore.Migrations
{
    /// <inheritdoc />
    public partial class ModelsBuilders : Migration
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
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Transacciones",
                columns: table => new
                {
                    TransaccionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacturaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacciones", x => x.TransaccionId);
                    table.ForeignKey(
                        name: "FK_Transacciones_FacturaAdmins_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "FacturaAdmins",
                        principalColumn: "FacturaAdminId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transacciones_Facturas_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Facturas",
                        principalColumn: "FacturaId",
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
                    { 1, "admin1@gmail.com", "Calle Principal #1", new DateTime(2020, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "1234567890123456789", "Qwe123...", "Admin", "8091234567", "Admin1" },
                    { 2, "user1@gmail.com", "Avenida Siempre Viva #2", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "9876543210987654321", "Asd123...", "User", "8092345678", "User1" },
                    { 3, "admin2@gmail.com", "Calle Luna #3", new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "1111222233334444555", "Admin234...", "Admin", "8093456789", "Admin2" },
                    { 4, "user2@gmail.com", "Calle Sol #4", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "5555666677778888999", "User234...", "User", "8094567890", "User2" },
                    { 5, "admin3@gmail.com", "Calle 5 de Abril", new DateTime(2022, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "1122334455667788990", "Admin345...", "Admin", "8291234567", "Admin3" },
                    { 6, "user3@gmail.com", "Avenida Las Palmas", new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "9988776655443322110", "User345...", "User", "8292345678", "User3" },
                    { 7, "admin4@gmail.com", "Residencial Brisas del Este", new DateTime(2019, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "1234432112344321123", "Admin456...", "Admin", "8493456789", "Admin4" },
                    { 8, "user4@gmail.com", "Villa Mella, Santo Domingo", new DateTime(2021, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "3211233211233211234", "User456...", "User", "8494567890", "User4" },
                    { 9, "admin5@gmail.com", "Ensanche La Fe", new DateTime(2023, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "1231231231231231231", "Admin567...", "Admin", "8095678901", "Admin5" },
                    { 10, "user5@gmail.com", "Zona Colonial", new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "3213213213213213213", "User567...", "User", "8295678901", "User5" }
                });

            migrationBuilder.InsertData(
                table: "FacturaAdmins",
                columns: new[] { "FacturaAdminId", "FechaRegistro", "ProveedorId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 3, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 5 },
                    { 4, new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 7 },
                    { 5, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 9 }
                });

            migrationBuilder.InsertData(
                table: "Facturas",
                columns: new[] { "FacturaId", "FechaRegistro", "UsuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 2, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 3, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 4, new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 5, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 10 }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "ProductoId", "CategoriaId", "Descripcion", "FechaRegistro", "ImagenUrl", "Nombre", "PrecioUnitario", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "Teléfono móvil con pantalla AMOLED y triple cámara de alta resolución.", new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "imagenes/productos/samsung_s23.jpg", "Samsung Galaxy S23", 849.99m, 50 },
                    { 2, 1, "Teléfono inteligente con tecnología Face ID y cámara de 48 MP.", new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "imagenes/productos/iphone_14_pro.jpg", "iPhone 14 Pro", 1099.00m, 30 },
                    { 3, 2, "Laptop ultradelgada con pantalla táctil y procesador Intel i7.", new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "imagenes/productos/dell_xps_13.jpg", "Dell XPS 13", 1249.50m, 20 },
                    { 4, 2, "Computadora portátil con buen rendimiento para el trabajo diario.", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "imagenes/productos/hp_pavilion_15.jpg", "HP Pavilion 15", 799.99m, 40 },
                    { 5, 3, "Tableta de alto rendimiento con chip M1 y pantalla de 10.9 pulgadas.", new DateTime(2023, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "imagenes/productos/ipad_air_2022.jpg", "iPad Air 2022", 599.00m, 35 },
                    { 6, 3, "Tableta Android con gran rendimiento y pantalla AMOLED.", new DateTime(2023, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "imagenes/productos/galaxy_tab_s8.jpg", "Samsung Galaxy Tab S8", 649.99m, 25 },
                    { 7, 4, "Televisor OLED con resolución 4K y compatibilidad con Dolby Vision.", new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "imagenes/productos/lg_oled_c2.jpg", "LG OLED C2 55", 1399.00m, 15 },
                    { 8, 4, "Smart TV de 55 pulgadas con tecnología QLED y control por voz.", new DateTime(2023, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "imagenes/productos/samsung_qled_q70.jpg", "Samsung QLED Q70", 999.99m, 18 },
                    { 9, 5, "Cámara digital sin espejo ideal para fotografía profesional.", new DateTime(2023, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "imagenes/productos/canon_eos_m50.jpg", "Canon EOS M50", 699.00m, 22 },
                    { 10, 5, "Cámara compacta de lentes intercambiables con enfoque automático rápido.", new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "imagenes/productos/sony_a6400.jpg", "Sony Alpha a6400", 899.99m, 17 }
                });

            migrationBuilder.InsertData(
                table: "DetalleFacturaAdmins",
                columns: new[] { "DetalleFacturaAdminId", "Cantidad", "FacturaAdminId", "PrecioUnitario", "ProductoId" },
                values: new object[,]
                {
                    { 1, 50, 1, 399.99m, 1 },
                    { 2, 30, 1, 599.50m, 2 },
                    { 3, 45, 2, 349.99m, 3 },
                    { 4, 60, 2, 799.99m, 4 },
                    { 5, 20, 3, 259.99m, 5 },
                    { 6, 70, 3, 1199.00m, 6 },
                    { 7, 40, 4, 499.99m, 7 },
                    { 8, 35, 4, 1399.00m, 8 },
                    { 9, 60, 5, 129.99m, 9 },
                    { 10, 25, 5, 899.99m, 10 }
                });

            migrationBuilder.InsertData(
                table: "DetalleFacturas",
                columns: new[] { "DetalleFacturaId", "Cantidad", "FacturaId", "PrecioUnitario", "ProductoId", "Subtotal" },
                values: new object[,]
                {
                    { 1, 2, 1, 0m, 1, 200m },
                    { 2, 1, 1, 0m, 3, 150m },
                    { 3, 2, 2, 0m, 2, 160m },
                    { 4, 1, 2, 0m, 4, 150m },
                    { 5, 1, 3, 0m, 3, 150m },
                    { 6, 2, 3, 0m, 5, 200m },
                    { 7, 2, 4, 0m, 1, 40m },
                    { 8, 2, 4, 0m, 2, 160m },
                    { 9, 2, 5, 0m, 3, 300m },
                    { 10, 1, 5, 0m, 5, 40m }
                });

            migrationBuilder.InsertData(
                table: "Transacciones",
                columns: new[] { "TransaccionId", "FacturaId", "FechaRegistro", "Monto", "Tipo" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 37984.50m, "Gasto" },
                    { 2, 2, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 63748.95m, "Gasto" },
                    { 3, 3, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 89129.80m, "Gasto" },
                    { 4, 4, new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 68964.60m, "Gasto" },
                    { 5, 5, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 30299.15m, "Gasto" },
                    { 6, 1, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 350.00m, "Ingreso" },
                    { 7, 2, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 310.00m, "Ingreso" },
                    { 8, 3, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 350.00m, "Ingreso" },
                    { 9, 4, new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 200.00m, "Ingreso" },
                    { 10, 5, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 340.00m, "Ingreso" }
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

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_FacturaId",
                table: "Transacciones",
                column: "FacturaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleFacturaAdmins");

            migrationBuilder.DropTable(
                name: "DetalleFacturas");

            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "FacturaAdmins");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
