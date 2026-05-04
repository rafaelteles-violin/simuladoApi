using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simulado.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addcamposdisciplina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoDisciplina",
                table: "Disciplina",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalExibicaoQuestao",
                table: "Disciplina",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoDisciplina",
                table: "Disciplina");

            migrationBuilder.DropColumn(
                name: "TotalExibicaoQuestao",
                table: "Disciplina");
        }
    }
}
