using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorRent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entregadores",
                columns: table => new
                {
                    Identificador = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Cnpj = table.Column<string>(type: "text", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumeroCnh = table.Column<string>(type: "text", nullable: false),
                    TipoCnh = table.Column<int>(type: "integer", nullable: false),
                    ImagemCnhPath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entregadores", x => x.Identificador);
                });

            migrationBuilder.CreateTable(
                name: "Motos",
                columns: table => new
                {
                    Identificador = table.Column<string>(type: "text", nullable: false),
                    Modelo = table.Column<string>(type: "text", nullable: false),
                    Ano = table.Column<int>(type: "integer", nullable: false),
                    Placa = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motos", x => x.Identificador);
                });

            migrationBuilder.CreateTable(
                name: "Locacoes",
                columns: table => new
                {
                    Identificador = table.Column<string>(type: "text", nullable: false),
                    EntregadorId = table.Column<string>(type: "text", nullable: false),
                    MotoId = table.Column<string>(type: "text", nullable: false),
                    Plano = table.Column<int>(type: "integer", nullable: false),
                    ValorDiaria = table.Column<decimal>(type: "numeric", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataPrevisaoTermino = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataTermino = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TotalPago = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locacoes", x => x.Identificador);
                    table.ForeignKey(
                        name: "FK_Locacoes_Entregadores_EntregadorId",
                        column: x => x.EntregadorId,
                        principalTable: "Entregadores",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locacoes_Motos_MotoId",
                        column: x => x.MotoId,
                        principalTable: "Motos",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entregadores_Identificador",
                table: "Entregadores",
                column: "Identificador");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_EntregadorId",
                table: "Locacoes",
                column: "EntregadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_Identificador",
                table: "Locacoes",
                column: "Identificador");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_MotoId",
                table: "Locacoes",
                column: "MotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Motos_Identificador",
                table: "Motos",
                column: "Identificador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locacoes");

            migrationBuilder.DropTable(
                name: "Entregadores");

            migrationBuilder.DropTable(
                name: "Motos");
        }
    }
}
