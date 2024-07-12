using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obecni_clone.Server.Migrations
{
    /// <inheritdoc />
    public partial class ForeginKeysCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klienci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naziwsko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Miasto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kod_pocztowy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klienci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pracownicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pseudonim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataZatrudnieniaOd = table.Column<DateOnly>(type: "date", nullable: false),
                    DataZatrudnieniaDo = table.Column<DateOnly>(type: "date", nullable: true),
                    Moderator = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownicy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HDs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    Dzien = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HDs_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rejestry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    Wejscie = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Wyjscie = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Status2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rejestry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rejestry_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Urlopy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    Od_Kiedy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Do_Kiedy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DlugoscUrlopu = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urlopy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Urlopy_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WyjazdyKlienci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PracownikId = table.Column<int>(type: "int", nullable: false),
                    KlienciId = table.Column<int>(type: "int", nullable: false),
                    DataWyjazdu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GodzinaOd = table.Column<TimeSpan>(type: "time", nullable: true),
                    GodzinaDo = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WyjazdyKlienci", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WyjazdyKlienci_Klienci_KlienciId",
                        column: x => x.KlienciId,
                        principalTable: "Klienci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WyjazdyKlienci_Pracownicy_PracownikId",
                        column: x => x.PracownikId,
                        principalTable: "Pracownicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HDs_PracownikId",
                table: "HDs",
                column: "PracownikId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rejestry_PracownikId",
                table: "Rejestry",
                column: "PracownikId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Urlopy_PracownikId",
                table: "Urlopy",
                column: "PracownikId");

            migrationBuilder.CreateIndex(
                name: "IX_WyjazdyKlienci_KlienciId",
                table: "WyjazdyKlienci",
                column: "KlienciId");

            migrationBuilder.CreateIndex(
                name: "IX_WyjazdyKlienci_PracownikId",
                table: "WyjazdyKlienci",
                column: "PracownikId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HDs");

            migrationBuilder.DropTable(
                name: "Rejestry");

            migrationBuilder.DropTable(
                name: "Urlopy");

            migrationBuilder.DropTable(
                name: "WyjazdyKlienci");

            migrationBuilder.DropTable(
                name: "Klienci");

            migrationBuilder.DropTable(
                name: "Pracownicy");
        }
    }
}
