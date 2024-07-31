using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalsasAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVentass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__DetalleVe__idMed__03F0984C",
                table: "DetalleVenta");

            migrationBuilder.RenameColumn(
                name: "idMedida",
                table: "DetalleVenta",
                newName: "MedidumIdMedida");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleVenta_idMedida",
                table: "DetalleVenta",
                newName: "IX_DetalleVenta_MedidumIdMedida");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleVenta_Medida_MedidumIdMedida",
                table: "DetalleVenta",
                column: "MedidumIdMedida",
                principalTable: "Medida",
                principalColumn: "idMedida");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleVenta_Medida_MedidumIdMedida",
                table: "DetalleVenta");

            migrationBuilder.RenameColumn(
                name: "MedidumIdMedida",
                table: "DetalleVenta",
                newName: "idMedida");

            migrationBuilder.RenameIndex(
                name: "IX_DetalleVenta_MedidumIdMedida",
                table: "DetalleVenta",
                newName: "IX_DetalleVenta_idMedida");

            migrationBuilder.AddForeignKey(
                name: "FK__DetalleVe__idMed__03F0984C",
                table: "DetalleVenta",
                column: "idMedida",
                principalTable: "Medida",
                principalColumn: "idMedida");
        }
    }
}
