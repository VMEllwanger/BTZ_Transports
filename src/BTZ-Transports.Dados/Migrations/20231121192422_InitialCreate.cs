using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BTZ_Transports.Dados.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Combustiveis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combustiveis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motoristas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(512)", nullable: false),
                    CPF = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    NumeroCNH = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    CategoriaCNH = table.Column<string>(type: "VARCHAR(3)", nullable: false),
                    DataDeNascimento = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motoristas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(512)", nullable: false),
                    CombustivelId = table.Column<int>(type: "int", nullable: false),
                    Fabricante = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    AnoDeFabricacao = table.Column<int>(type: "int", nullable: false),
                    CapacidadeMaximaDoTanque = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observacao = table.Column<string>(type: "VARCHAR(1028)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculos_Combustiveis_CombustivelId",
                        column: x => x.CombustivelId,
                        principalTable: "Combustiveis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RegistrosAbastecimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CombustivelId = table.Column<int>(type: "int", nullable: false),
                    MotoristaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VeiculoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Data = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    QuantidadeAbastecida = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosAbastecimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosAbastecimento_Combustiveis_CombustivelId",
                        column: x => x.CombustivelId,
                        principalTable: "Combustiveis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegistrosAbastecimento_Motoristas_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "Motoristas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegistrosAbastecimento_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Combustiveis",
                columns: new[] { "Id", "Nome", "Preco" },
                values: new object[,]
                {
                    { 1, "Gasolina", 4.29m },
                    { 2, "Etanol", 2.99m },
                    { 3, "Diesel", 2.09m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosAbastecimento_CombustivelId",
                table: "RegistrosAbastecimento",
                column: "CombustivelId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosAbastecimento_MotoristaId",
                table: "RegistrosAbastecimento",
                column: "MotoristaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosAbastecimento_VeiculoId",
                table: "RegistrosAbastecimento",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_CombustivelId",
                table: "Veiculos",
                column: "CombustivelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosAbastecimento");

            migrationBuilder.DropTable(
                name: "Motoristas");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "Combustiveis");
        }
    }
}
