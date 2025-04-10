using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CyHTechnoStore.Migrations
{
    /// <inheritdoc />
    public partial class primera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productoss",
                columns: table => new
                {
                    ProductosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productoss", x => x.ProductosId);
                });

            migrationBuilder.CreateTable(
                name: "UserAccountss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    NumeroTarjeta = table.Column<string>(type: "nvarchar(19)", maxLength: 19, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facturass",
                columns: table => new
                {
                    FacturasId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturass", x => x.FacturasId);
                    table.ForeignKey(
                        name: "FK_Facturass_UserAccountss_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "UserAccountss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallesFacturas",
                columns: table => new
                {
                    DetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturasId = table.Column<int>(type: "int", nullable: false),
                    ProductosId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallesFacturas", x => x.DetalleId);
                    table.ForeignKey(
                        name: "FK_DetallesFacturas_Facturass_FacturasId",
                        column: x => x.FacturasId,
                        principalTable: "Facturass",
                        principalColumn: "FacturasId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetallesFacturas_Productoss_ProductosId",
                        column: x => x.ProductosId,
                        principalTable: "Productoss",
                        principalColumn: "ProductosId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserAccountss",
                columns: new[] { "Id", "Direccion", "FechaRegistro", "NumeroTarjeta", "Password", "Role", "Telefono", "UserName" },
                values: new object[,]
                {
                    { 1, "calle 18", new DateTime(2020, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "1234567891456978524", "Qwe123...", "Admin", "8094627895", "Administrador" },
                    { 2, "calle 890", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "999999999999999999", "Asd123...", "User", "8094627111", "Usuario" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesFacturas_FacturasId",
                table: "DetallesFacturas",
                column: "FacturasId");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesFacturas_ProductosId",
                table: "DetallesFacturas",
                column: "ProductosId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturass_UsuarioId",
                table: "Facturass",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallesFacturas");

            migrationBuilder.DropTable(
                name: "Facturass");

            migrationBuilder.DropTable(
                name: "Productoss");

            migrationBuilder.DropTable(
                name: "UserAccountss");
        }
    }
}
