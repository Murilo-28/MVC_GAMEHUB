using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_GAMEHUB.Migrations
{
    /// <inheritdoc />
    public partial class mundacasDNV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Preço",
                table: "Jogos",
                newName: "Preco");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Preco",
                table: "Jogos",
                newName: "Preço");
        }
    }
}
