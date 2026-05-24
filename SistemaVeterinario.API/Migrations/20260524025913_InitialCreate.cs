using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaVeterinario.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_TUTOR",
                columns: table => new
                {
                    ID_TUTOR = table.Column<decimal>(type: "DECIMAL(10,0)", precision: 10, scale: 0, nullable: false),
                    NOME_TUTOR = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    CPF_TUTOR = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    TELEFONE_TUTOR = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    EMAIL_TUTOR = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    CEP_TUTOR = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    RUA_TUTOR = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    NUMERO_TUTOR = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    COMPLEMENTO_TUTOR = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    BAIRRO_TUTOR = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    CIDADE_TUTOR = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ESTADO_TUTOR = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DATA_CADASTRO_TUTOR = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TUTOR", x => x.ID_TUTOR);
                });

            migrationBuilder.CreateTable(
                name: "TB_VETERINARIO",
                columns: table => new
                {
                    ID_VETERINARIO = table.Column<decimal>(type: "DECIMAL(10,0)", precision: 10, scale: 0, nullable: false),
                    NOME_VETERINARIO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CRM_VETERINARIO = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    ESPECIALIDADE_VETERINARIO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    TELEFONE_VETERINARIO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    EMAIL_VETERINARIO = table.Column<string>(type: "NVARCHAR2(450)", nullable: true),
                    STATUS_VETERINARIO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DATA_CADASTRO_VETERINARIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_VETERINARIO", x => x.ID_VETERINARIO);
                });

            migrationBuilder.CreateTable(
                name: "TB_PET",
                columns: table => new
                {
                    ID_PET = table.Column<decimal>(type: "DECIMAL(10,0)", precision: 10, scale: 0, nullable: false),
                    ID_TUTOR = table.Column<decimal>(type: "DECIMAL(10,0)", precision: 10, scale: 0, nullable: false),
                    NOME_PET = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ESPECIE_PET = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    RACA_PET = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    SEXO_PET = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    DATA_NASCIMENTO_PET = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    PESO_PET = table.Column<decimal>(type: "DECIMAL(5,2)", precision: 5, scale: 2, nullable: false),
                    COR_PET = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    DATA_CADASTRO_PET = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PET", x => x.ID_PET);
                    table.ForeignKey(
                        name: "FK_TB_PET_TB_TUTOR_ID_TUTOR",
                        column: x => x.ID_TUTOR,
                        principalTable: "TB_TUTOR",
                        principalColumn: "ID_TUTOR",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_PET_ID_TUTOR",
                table: "TB_PET",
                column: "ID_TUTOR");

            migrationBuilder.CreateIndex(
                name: "IX_TB_VETERINARIO_CRM_VETERINARIO",
                table: "TB_VETERINARIO",
                column: "CRM_VETERINARIO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_VETERINARIO_EMAIL_VETERINARIO",
                table: "TB_VETERINARIO",
                column: "EMAIL_VETERINARIO",
                unique: true,
                filter: "\"EMAIL_VETERINARIO\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PET");

            migrationBuilder.DropTable(
                name: "TB_VETERINARIO");

            migrationBuilder.DropTable(
                name: "TB_TUTOR");
        }
    }
}
