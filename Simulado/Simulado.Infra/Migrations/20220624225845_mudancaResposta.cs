using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Simulado.Infra.Migrations
{
    public partial class mudancaResposta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resposta_Usuario_UsuarioId",
                table: "Resposta");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Resposta",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Aluno",
                table: "Resposta",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Selecionada",
                table: "AlternativaResposta",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Resposta_Usuario_UsuarioId",
                table: "Resposta",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resposta_Usuario_UsuarioId",
                table: "Resposta");

            migrationBuilder.DropColumn(
                name: "Aluno",
                table: "Resposta");

            migrationBuilder.DropColumn(
                name: "Selecionada",
                table: "AlternativaResposta");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Resposta",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Resposta_Usuario_UsuarioId",
                table: "Resposta",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
