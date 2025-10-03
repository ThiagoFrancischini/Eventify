using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eventify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tabelasIngressos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "interval", nullable: false),
                    HoraFim = table.Column<TimeSpan>(type: "interval", nullable: false),
                    MaisSobre = table.Column<string>(type: "text", nullable: false),
                    ImagemEvento = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Categoria = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EnderecoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoriasIngresso",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    LimiteCompra = table.Column<int>(type: "integer", nullable: false),
                    EventoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasIngresso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoriasIngresso_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingressos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioCompraId = table.Column<Guid>(type: "uuid", nullable: false),
                    Valido = table.Column<bool>(type: "boolean", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataUso = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Codigo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EventoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoriaIngressoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingressos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingressos_CategoriasIngresso_CategoriaIngressoId",
                        column: x => x.CategoriaIngressoId,
                        principalTable: "CategoriasIngresso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingressos_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingressos_Usuarios_UsuarioCompraId",
                        column: x => x.UsuarioCompraId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasIngresso_EventoId",
                table: "CategoriasIngresso",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_EnderecoId",
                table: "Eventos",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingressos_CategoriaIngressoId",
                table: "Ingressos",
                column: "CategoriaIngressoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingressos_EventoId",
                table: "Ingressos",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingressos_UsuarioCompraId",
                table: "Ingressos",
                column: "UsuarioCompraId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingressos");

            migrationBuilder.DropTable(
                name: "CategoriasIngresso");

            migrationBuilder.DropTable(
                name: "Eventos");
        }
    }
}
