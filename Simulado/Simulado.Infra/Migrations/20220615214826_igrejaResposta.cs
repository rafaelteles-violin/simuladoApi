using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Simulado.Infra.Migrations
{
    public partial class igrejaResposta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IgrejaId",
                table: "RespostaSaldo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RespostaSaldo_IgrejaId",
                table: "RespostaSaldo",
                column: "IgrejaId");

            migrationBuilder.AddForeignKey(
                name: "FK_RespostaSaldo_Igreja_IgrejaId",
                table: "RespostaSaldo",
                column: "IgrejaId",
                principalTable: "Igreja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RespostaSaldo_Igreja_IgrejaId",
                table: "RespostaSaldo");

            migrationBuilder.DropIndex(
                name: "IX_RespostaSaldo_IgrejaId",
                table: "RespostaSaldo");

            migrationBuilder.DropColumn(
                name: "IgrejaId",
                table: "RespostaSaldo");
        }
    }
}
