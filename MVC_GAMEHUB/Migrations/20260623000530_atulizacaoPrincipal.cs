using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_GAMEHUB.Migrations
{
    /// <inheritdoc />
    public partial class atulizacaoPrincipal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Jogos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Jogos");
        }
    }
}
