using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ajustePropsTabelaEvento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaisSobre",
                table: "Eventos");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Eventos",
                newName: "DataTermino");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "Eventos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "Eventos");

            migrationBuilder.RenameColumn(
                name: "DataTermino",
                table: "Eventos",
                newName: "Data");

            migrationBuilder.AddColumn<string>(
                name: "MaisSobre",
                table: "Eventos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
