using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmeApi.Migrations
{
    /// <inheritdoc />
    public partial class CinemaeEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Cinemas",
                newName: "NomeCinema");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeCinema",
                table: "Cinemas",
                newName: "Nome");
        }
    }
}
