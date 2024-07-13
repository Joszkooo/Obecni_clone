﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obecni_clone.Server.Migrations
{
    /// <inheritdoc />
    public partial class newServicesAndMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Naziwsko",
                table: "Klienci",
                newName: "Nazwisko");

            migrationBuilder.CreateTable(
                name: "DniWolne",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dzien = table.Column<DateOnly>(type: "date", nullable: false),
                    NazwaSwieta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DniWolne", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DniWolne");

            migrationBuilder.RenameColumn(
                name: "Nazwisko",
                table: "Klienci",
                newName: "Naziwsko");
        }
    }
}
