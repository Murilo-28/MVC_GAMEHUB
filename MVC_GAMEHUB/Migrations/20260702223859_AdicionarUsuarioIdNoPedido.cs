using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_GAMEHUB.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarUsuarioIdNoPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Pedidos");
        }
    }
}
