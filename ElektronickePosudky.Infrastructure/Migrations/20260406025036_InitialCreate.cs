using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElektronickePosudky.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CiselnikPolozky",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CiselnikKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Verze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RodicId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrekladyJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CiselnikPolozky", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ciselniky",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Verze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlatnostOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlatnostDo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrekladyJson = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciselniky", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PosudkyRo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Hlavicka_Pacient_Rid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hlavicka_Pacient_Jmeno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hlavicka_Pacient_Prijmeni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hlavicka_Pacient_DatumNarozeni = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hlavicka_Pacient_Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hlavicka_Pacient_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hlavicka_Pacient_Doklad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hlavicka_Pacient_Pohlavi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hlavicka_ZdravotnickyPracovnik_KrzpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hlavicka_ZdravotnickyPracovnik_TitulPred = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hlavicka_ZdravotnickyPracovnik_Jmeno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hlavicka_ZdravotnickyPracovnik_Prijmeni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hlavicka_ZdravotnickyPracovnik_TitulZa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hlavicka_ZdravotnickyPracovnik_Odbornost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hlavicka_PoskytovatelZdravotnickychSluzeb_Ico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hlavicka_PoskytovatelZdravotnickychSluzeb_Nazev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hlavicka_PoskytovatelZdravotnickychSluzeb_Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hlavicka_OdbornostLekare_CiselnikKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hlavicka_OdbornostLekare_CiselnikVerze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hlavicka_OdbornostLekare_PolozkaKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hlavicka_StavPosudku_CiselnikKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hlavicka_StavPosudku_CiselnikVerze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hlavicka_StavPosudku_PolozkaKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hlavicka_DruhProhlidky_CiselnikKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hlavicka_DruhProhlidky_CiselnikVerze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hlavicka_DruhProhlidky_PolozkaKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hlavicka_DruhPosudku_CiselnikKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hlavicka_DruhPosudku_CiselnikVerze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hlavicka_DruhPosudku_PolozkaKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hlavicka_DatumVystaveni = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hlavicka_PlatnostDo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hlavicka_DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hlavicka_VerzeZaznamu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosudkyRo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PosudkyRo_Historie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PosudekRoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypOperace_CiselnikKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypOperace_CiselnikVerze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypOperace_PolozkaKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumOperace = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lekar_KrzpId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lekar_TitulPred = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lekar_Jmeno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lekar_Prijmeni = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lekar_TitulZa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lekar_Odbornost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Poskytovatel_Ico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Poskytovatel_Nazev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Poskytovatel_Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosudkyRo_Historie", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosudkyRo_Historie_PosudkyRo_PosudekRoId",
                        column: x => x.PosudekRoId,
                        principalTable: "PosudkyRo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PosudkyRo_Zpusobilosti",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PosudekRoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkupinaZadateleRidic_CiselnikKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkupinaZadateleRidic_CiselnikVerze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkupinaZadateleRidic_PolozkaKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vysledek_CiselnikKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vysledek_CiselnikVerze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vysledek_PolozkaKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerzeZaznamu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosudkyRo_Zpusobilosti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosudkyRo_Zpusobilosti_PosudkyRo_PosudekRoId",
                        column: x => x.PosudekRoId,
                        principalTable: "PosudkyRo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PosudkyRo_HarmonizovaneKody",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PosudekZpusobilostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HarmonizovanyKod_CiselnikKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HarmonizovanyKod_CiselnikVerze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HarmonizovanyKod_PolozkaKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpresneniKod_CiselnikKod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpresneniKod_CiselnikVerze = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpresneniKod_PolozkaKod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpresneniText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosudkyRo_HarmonizovaneKody", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosudkyRo_HarmonizovaneKody_PosudkyRo_Zpusobilosti_PosudekZpusobilostId",
                        column: x => x.PosudekZpusobilostId,
                        principalTable: "PosudkyRo_Zpusobilosti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PosudkyRo_NarodniKody",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PosudekZpusobilostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NarodniKod_CiselnikKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NarodniKod_CiselnikVerze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NarodniKod_PolozkaKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkupinaRo_CiselnikKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkupinaRo_CiselnikVerze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkupinaRo_PolozkaKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpresneniText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosudkyRo_NarodniKody", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosudkyRo_NarodniKody_PosudkyRo_Zpusobilosti_PosudekZpusobilostId",
                        column: x => x.PosudekZpusobilostId,
                        principalTable: "PosudkyRo_Zpusobilosti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PosudkyRo_SkupinyRO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PosudekZpusobilostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkupinaRo_CiselnikKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkupinaRo_CiselnikVerze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkupinaRo_PolozkaKod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosudkyRo_SkupinyRO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosudkyRo_SkupinyRO_PosudkyRo_Zpusobilosti_PosudekZpusobilostId",
                        column: x => x.PosudekZpusobilostId,
                        principalTable: "PosudkyRo_Zpusobilosti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PosudkyRo_HarmonizovaneKody_SkupinyRO_Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CiselnikKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CiselnikVerze = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolozkaKod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosudekHarmonizovanyKodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosudkyRo_HarmonizovaneKody_SkupinyRO_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosudkyRo_HarmonizovaneKody_SkupinyRO_Items_PosudkyRo_HarmonizovaneKody_PosudekHarmonizovanyKodId",
                        column: x => x.PosudekHarmonizovanyKodId,
                        principalTable: "PosudkyRo_HarmonizovaneKody",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PosudkyRo_HarmonizovaneKody_PosudekZpusobilostId",
                table: "PosudkyRo_HarmonizovaneKody",
                column: "PosudekZpusobilostId");

            migrationBuilder.CreateIndex(
                name: "IX_PosudkyRo_HarmonizovaneKody_SkupinyRO_Items_PosudekHarmonizovanyKodId",
                table: "PosudkyRo_HarmonizovaneKody_SkupinyRO_Items",
                column: "PosudekHarmonizovanyKodId");

            migrationBuilder.CreateIndex(
                name: "IX_PosudkyRo_Historie_PosudekRoId",
                table: "PosudkyRo_Historie",
                column: "PosudekRoId");

            migrationBuilder.CreateIndex(
                name: "IX_PosudkyRo_NarodniKody_PosudekZpusobilostId",
                table: "PosudkyRo_NarodniKody",
                column: "PosudekZpusobilostId");

            migrationBuilder.CreateIndex(
                name: "IX_PosudkyRo_SkupinyRO_PosudekZpusobilostId",
                table: "PosudkyRo_SkupinyRO",
                column: "PosudekZpusobilostId");

            migrationBuilder.CreateIndex(
                name: "IX_PosudkyRo_Zpusobilosti_PosudekRoId",
                table: "PosudkyRo_Zpusobilosti",
                column: "PosudekRoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CiselnikPolozky");

            migrationBuilder.DropTable(
                name: "Ciselniky");

            migrationBuilder.DropTable(
                name: "PosudkyRo_HarmonizovaneKody_SkupinyRO_Items");

            migrationBuilder.DropTable(
                name: "PosudkyRo_Historie");

            migrationBuilder.DropTable(
                name: "PosudkyRo_NarodniKody");

            migrationBuilder.DropTable(
                name: "PosudkyRo_SkupinyRO");

            migrationBuilder.DropTable(
                name: "PosudkyRo_HarmonizovaneKody");

            migrationBuilder.DropTable(
                name: "PosudkyRo_Zpusobilosti");

            migrationBuilder.DropTable(
                name: "PosudkyRo");
        }
    }
}
