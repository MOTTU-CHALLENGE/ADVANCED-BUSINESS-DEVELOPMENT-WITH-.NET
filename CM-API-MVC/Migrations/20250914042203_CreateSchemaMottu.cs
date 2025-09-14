using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CM_API_MVC.Migrations
{
    /// <inheritdoc />
    public partial class CreateSchemaMottu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "T_CM_FILIAL",
                columns: table => new
                {
                    ID_FILIAL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NM_FILIAL = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DS_ENDERECO = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DS_CIDADE = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DS_ESTADO = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DS_PAIS = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CD_CEP = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DS_TELEFONE = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DT_INAUGURACAO = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CM_FILIAL", x => x.ID_FILIAL);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "T_CM_PATIO",
                columns: table => new
                {
                    ID_PATIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_FILIAL = table.Column<int>(type: "int", nullable: false),
                    NM_PATIO = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NR_CAP_MAX = table.Column<int>(type: "int", nullable: true),
                    VL_AREA_PATIO = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    DS_OBS = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CM_PATIO", x => x.ID_PATIO);
                    table.ForeignKey(
                        name: "FK_T_CM_PATIO_T_CM_FILIAL_ID_FILIAL",
                        column: x => x.ID_FILIAL,
                        principalTable: "T_CM_FILIAL",
                        principalColumn: "ID_FILIAL",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "T_CM_RECEPTOR_WIFI",
                columns: table => new
                {
                    ID_LEITOR = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_PATIO = table.Column<int>(type: "int", nullable: false),
                    DS_LOCAL_INSTALACAO = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DS_ENDERECO_MAC = table.Column<string>(type: "varchar(17)", maxLength: 17, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ST_STATUS = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DT_INSTALACAO = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DS_OBS = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CM_RECEPTOR_WIFI", x => x.ID_LEITOR);
                    table.ForeignKey(
                        name: "FK_T_CM_RECEPTOR_WIFI_T_CM_PATIO_ID_PATIO",
                        column: x => x.ID_PATIO,
                        principalTable: "T_CM_PATIO",
                        principalColumn: "ID_PATIO",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "T_CM_DISPOSITIVO_IOT",
                columns: table => new
                {
                    ID_DISPOSITIVO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NM_DISPOSITIVO = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DT_INSTALACAO = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DS_OBS = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ID_MOTO = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CM_DISPOSITIVO_IOT", x => x.ID_DISPOSITIVO);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "T_CM_POSICAO_MOTO",
                columns: table => new
                {
                    ID_POSICAO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_DISPOSITIVO = table.Column<int>(type: "int", nullable: false),
                    ID_PATIO = table.Column<int>(type: "int", nullable: false),
                    COORD_X = table.Column<double>(type: "double", nullable: false),
                    COORD_Y = table.Column<double>(type: "double", nullable: false),
                    DT_REGISTRO = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DS_SETOR = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CM_POSICAO_MOTO", x => x.ID_POSICAO);
                    table.ForeignKey(
                        name: "FK_T_CM_POSICAO_MOTO_T_CM_DISPOSITIVO_IOT_ID_DISPOSITIVO",
                        column: x => x.ID_DISPOSITIVO,
                        principalTable: "T_CM_DISPOSITIVO_IOT",
                        principalColumn: "ID_DISPOSITIVO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_CM_POSICAO_MOTO_T_CM_PATIO_ID_PATIO",
                        column: x => x.ID_PATIO,
                        principalTable: "T_CM_PATIO",
                        principalColumn: "ID_PATIO",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "T_CM_MOTO",
                columns: table => new
                {
                    ID_MOTO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CD_TAG = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TP_MOTO = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DS_PLACA = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ST_STATUS = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DT_CADASTRO = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NR_ANO_FABRICACAO = table.Column<int>(type: "int", nullable: true),
                    DS_MODELO = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CM_MOTO", x => x.ID_MOTO);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "T_CM_RFID",
                columns: table => new
                {
                    CD_TAG = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VL_FREQUENCIA = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ST_STATUS = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DT_ATIVACAO = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DS_OBS = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodTag = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CM_RFID", x => x.CD_TAG);
                    table.ForeignKey(
                        name: "FK_T_CM_RFID_T_CM_MOTO_CodTag",
                        column: x => x.CodTag,
                        principalTable: "T_CM_MOTO",
                        principalColumn: "ID_MOTO");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_T_CM_DISPOSITIVO_IOT_ID_MOTO",
                table: "T_CM_DISPOSITIVO_IOT",
                column: "ID_MOTO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_CM_MOTO_CD_TAG",
                table: "T_CM_MOTO",
                column: "CD_TAG",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_CM_PATIO_ID_FILIAL",
                table: "T_CM_PATIO",
                column: "ID_FILIAL");

            migrationBuilder.CreateIndex(
                name: "IX_T_CM_POSICAO_MOTO_ID_DISPOSITIVO",
                table: "T_CM_POSICAO_MOTO",
                column: "ID_DISPOSITIVO");

            migrationBuilder.CreateIndex(
                name: "IX_T_CM_POSICAO_MOTO_ID_PATIO",
                table: "T_CM_POSICAO_MOTO",
                column: "ID_PATIO");

            migrationBuilder.CreateIndex(
                name: "IX_T_CM_RECEPTOR_WIFI_ID_PATIO",
                table: "T_CM_RECEPTOR_WIFI",
                column: "ID_PATIO");

            migrationBuilder.CreateIndex(
                name: "IX_T_CM_RFID_CodTag",
                table: "T_CM_RFID",
                column: "CodTag");

            migrationBuilder.AddForeignKey(
                name: "FK_T_CM_DISPOSITIVO_IOT_T_CM_MOTO_ID_MOTO",
                table: "T_CM_DISPOSITIVO_IOT",
                column: "ID_MOTO",
                principalTable: "T_CM_MOTO",
                principalColumn: "ID_MOTO",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_T_CM_MOTO_T_CM_RFID_CD_TAG",
                table: "T_CM_MOTO",
                column: "CD_TAG",
                principalTable: "T_CM_RFID",
                principalColumn: "CD_TAG",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_CM_RFID_T_CM_MOTO_CodTag",
                table: "T_CM_RFID");

            migrationBuilder.DropTable(
                name: "T_CM_POSICAO_MOTO");

            migrationBuilder.DropTable(
                name: "T_CM_RECEPTOR_WIFI");

            migrationBuilder.DropTable(
                name: "T_CM_DISPOSITIVO_IOT");

            migrationBuilder.DropTable(
                name: "T_CM_PATIO");

            migrationBuilder.DropTable(
                name: "T_CM_FILIAL");

            migrationBuilder.DropTable(
                name: "T_CM_MOTO");

            migrationBuilder.DropTable(
                name: "T_CM_RFID");
        }
    }
}
