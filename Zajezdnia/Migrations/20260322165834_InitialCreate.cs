using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zajezdnia.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kierowcy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NrPrawazJazdy = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DataZatrudnienia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NrTelefonu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kierowcy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zajezdnie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Miasto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zajezdnie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Autobusy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumerRejestracyjny = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Marka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RokProdukcji = table.Column<int>(type: "int", nullable: false),
                    LiczbaMiejsc = table.Column<int>(type: "int", nullable: false),
                    Kolor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZajezdniaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autobusy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Autobusy_Zajezdnie_ZajezdniaId",
                        column: x => x.ZajezdniaId,
                        principalTable: "Zajezdnie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutobusKierowca",
                columns: table => new
                {
                    AutobusyId = table.Column<int>(type: "int", nullable: false),
                    KierowcyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutobusKierowca", x => new { x.AutobusyId, x.KierowcyId });
                    table.ForeignKey(
                        name: "FK_AutobusKierowca_Autobusy_AutobusyId",
                        column: x => x.AutobusyId,
                        principalTable: "Autobusy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutobusKierowca_Kierowcy_KierowcyId",
                        column: x => x.KierowcyId,
                        principalTable: "Kierowcy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kursy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataOdjazdu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumerLinii = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Trasa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KierowcaId = table.Column<int>(type: "int", nullable: false),
                    AutobusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kursy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kursy_Autobusy_AutobusId",
                        column: x => x.AutobusId,
                        principalTable: "Autobusy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kursy_Kierowcy_KierowcaId",
                        column: x => x.KierowcaId,
                        principalTable: "Kierowcy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutobusKierowca_KierowcyId",
                table: "AutobusKierowca",
                column: "KierowcyId");

            migrationBuilder.CreateIndex(
                name: "IX_Autobusy_ZajezdniaId",
                table: "Autobusy",
                column: "ZajezdniaId");

            migrationBuilder.CreateIndex(
                name: "IX_Kursy_AutobusId",
                table: "Kursy",
                column: "AutobusId");

            migrationBuilder.CreateIndex(
                name: "IX_Kursy_KierowcaId",
                table: "Kursy",
                column: "KierowcaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutobusKierowca");

            migrationBuilder.DropTable(
                name: "Kursy");

            migrationBuilder.DropTable(
                name: "Autobusy");

            migrationBuilder.DropTable(
                name: "Kierowcy");

            migrationBuilder.DropTable(
                name: "Zajezdnie");
        }
    }
}
