using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalsasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEncuestaSatisfaccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Usuario_AgenteIdUsuario",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_AgenteIdUsuario",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "AgenteIdUsuario",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "IdAgente",
                table: "Usuario");

            migrationBuilder.CreateTable(
                name: "AgentesVenta",
                columns: table => new
                {
                    IdAgentesVenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAgente = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentesVenta", x => x.IdAgentesVenta);
                    table.ForeignKey(
                        name: "FK_AgentesVenta_Usuario_IdAgente",
                        column: x => x.IdAgente,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgentesVenta_Usuario_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Usuario",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentesVenta_IdAgente",
                table: "AgentesVenta",
                column: "IdAgente");

            migrationBuilder.CreateIndex(
                name: "IX_AgentesVenta_IdCliente",
                table: "AgentesVenta",
                column: "IdCliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentesVenta");

            migrationBuilder.AddColumn<int>(
                name: "AgenteIdUsuario",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdAgente",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_AgenteIdUsuario",
                table: "Usuario",
                column: "AgenteIdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Usuario_AgenteIdUsuario",
                table: "Usuario",
                column: "AgenteIdUsuario",
                principalTable: "Usuario",
                principalColumn: "idUsuario");
        }
    }
}
