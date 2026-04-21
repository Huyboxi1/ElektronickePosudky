using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElektronickePosudky.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTypAkceColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hlavicka_TypAkce_CiselnikKod",
                table: "PosudkyRo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Hlavicka_TypAkce_CiselnikVerze",
                table: "PosudkyRo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Hlavicka_TypAkce_PolozkaKod",
                table: "PosudkyRo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hlavicka_TypAkce_CiselnikKod",
                table: "PosudkyRo");

            migrationBuilder.DropColumn(
                name: "Hlavicka_TypAkce_CiselnikVerze",
                table: "PosudkyRo");

            migrationBuilder.DropColumn(
                name: "Hlavicka_TypAkce_PolozkaKod",
                table: "PosudkyRo");
        }
    }
}
