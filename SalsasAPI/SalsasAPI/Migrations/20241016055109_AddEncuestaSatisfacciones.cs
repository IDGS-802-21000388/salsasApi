using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalsasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEncuestaSatisfacciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncuestaSatisfaccion",
                columns: table => new
                {
                    idEncuesta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idVenta = table.Column<int>(type: "int", nullable: false),
                    procesoCompra = table.Column<byte>(type: "tinyint", nullable: false),
                    saborProducto = table.Column<byte>(type: "tinyint", nullable: false),
                    entregaProducto = table.Column<byte>(type: "tinyint", nullable: false),
                    presentacionProducto = table.Column<byte>(type: "tinyint", nullable: false),
                    facilidadUsoPagina = table.Column<byte>(type: "tinyint", nullable: false),
                    fechaEncuesta = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EncuestaSatisfaccion__IdEncuesta", x => x.idEncuesta);
                    table.ForeignKey(
                        name: "FK_Usuario_Encuesta",
                        column: x => x.idUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario");
                    table.ForeignKey(
                        name: "FK_Venta_Encuesta",
                        column: x => x.idVenta,
                        principalTable: "Venta",
                        principalColumn: "idVenta");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaSatisfaccion_idUsuario",
                table: "EncuestaSatisfaccion",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaSatisfaccion_idVenta",
                table: "EncuestaSatisfaccion",
                column: "idVenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncuestaSatisfaccion");
        }
    }
}
