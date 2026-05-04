using Microsoft.EntityFrameworkCore.Migrations;

namespace Simulado.Infra.Migrations
{
    public partial class identificador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identificador",
                table: "RespostaSaldo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identificador",
                table: "Resposta",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "RespostaSaldo");

            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "Resposta");
        }
    }
}
