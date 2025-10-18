using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MinhaNovaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioCriacaoId",
                table: "Eventos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_UsuarioCriacaoId",
                table: "Eventos",
                column: "UsuarioCriacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Eventos_Usuarios_UsuarioCriacaoId",
                table: "Eventos",
                column: "UsuarioCriacaoId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Eventos_Usuarios_UsuarioCriacaoId",
                table: "Eventos");

            migrationBuilder.DropIndex(
                name: "IX_Eventos_UsuarioCriacaoId",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "UsuarioCriacaoId",
                table: "Eventos");
        }
    }
}
