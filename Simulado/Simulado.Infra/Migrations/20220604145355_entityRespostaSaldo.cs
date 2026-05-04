using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Simulado.Infra.Migrations
{
    public partial class entityRespostaSaldo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RespostaSaldo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCandidato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAcerto = table.Column<int>(type: "int", nullable: false),
                    TotalErro = table.Column<int>(type: "int", nullable: false),
                    TotalQuestao = table.Column<int>(type: "int", nullable: false),
                    DisciplinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lixeira = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostaSaldo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RespostaSaldo_Disciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RespostaSaldo_DisciplinaId",
                table: "RespostaSaldo",
                column: "DisciplinaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RespostaSaldo");
        }
    }
}
