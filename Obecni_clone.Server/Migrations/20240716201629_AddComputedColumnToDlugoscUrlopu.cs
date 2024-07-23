using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Obecni_clone.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddComputedColumnToDlugoscUrlopu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        // dropping tej kolumny przez prawdopodobienstwo wczesniejszego stworzenia jej w poprzednich migracjach
        migrationBuilder.DropColumn(
            name: "DlugoscUrlopu",
            table: "Urlopy");

            migrationBuilder.AddColumn<double>(
            name: "DlugoscUrlopu",
            table: "Urlopy",
            type: "float",
            nullable: false,
            computedColumnSql: "(datediff(day,[Od_Kiedy],[Do_Kiedy]))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "DlugoscUrlopu",
            table: "Urlopy");
        }
    }
}
