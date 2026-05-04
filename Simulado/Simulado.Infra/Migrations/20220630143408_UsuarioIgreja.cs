using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Simulado.Infra.Migrations
{
    public partial class UsuarioIgreja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Usuario",
                newName: "Telefone");

            migrationBuilder.CreateTable(
                name: "UsuarioIgreja",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IgrejaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lixeira = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioIgreja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioIgreja_Igreja_IgrejaId",
                        column: x => x.IgrejaId,
                        principalTable: "Igreja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioIgreja_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioIgreja_IgrejaId",
                table: "UsuarioIgreja",
                column: "IgrejaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioIgreja_UsuarioId",
                table: "UsuarioIgreja",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioIgreja");

            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "Usuario",
                newName: "Senha");
        }
    }
}
