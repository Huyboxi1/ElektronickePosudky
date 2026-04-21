using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElektronickePosudky.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedTermXData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CiselnikPolozky",
                columns: new[] { "Id", "CiselnikKod", "Kod", "PrekladyJson", "RodicId", "Verze" },
                values: new object[,]
                {
                    { new Guid("00c40302-fe85-d87f-52b3-856b3d982d2a"), "seznam-harmonizovane-kody-ro", "01.05", "{\"Name_cs\":\"01.05 O\\u010Dn\\u00ED kryt\",\"Description_cs\":\"01.05 O\\u010Dn\\u00ED kryt\"}", null, "1.0.0" },
                    { new Guid("023cbbe1-edd7-1745-6da0-699432ecc9b3"), "seznam-skupin-ro", "CE", "{\"Name_cs\":\"skupina CE\",\"Description_cs\":\"Skupina CE\"}", null, "1.0.0" },
                    { new Guid("0500d97e-4c22-6a25-0eec-3ad3fac4b757"), "stav-posudku", "stav_posudku_3", "{\"Name_cs\":\"zneplatn\\u011Bn\\u00FD\",\"Description_cs\":\"zneplatn\\u011Bn\\u00FD\"}", null, "1.0.0" },
                    { new Guid("06a40357-20dc-6b72-be4f-6473ad0f8d15"), "seznam-harmonizovane-kody-ro", "31.01", "{\"Name_cs\":\"31.01 Dal\\u0161\\u00ED sada paraleln\\u00EDch ped\\u00E1l\\u016F\",\"Description_cs\":\"31.01 Dal\\u0161\\u00ED sada paraleln\\u00EDch ped\\u00E1l\\u016F\"}", null, "1.0.0" },
                    { new Guid("06b4db25-d5f4-c7d5-22d6-cfaba8d77d6b"), "seznam-harmonizovane-kody-ro", "31.02", "{\"Name_cs\":\"31.02 Ped\\u00E1ly ve stejn\\u00E9 (nebo t\\u00E9m\\u011B\\u0159 stejn\\u00E9) \\u00FArovni\",\"Description_cs\":\"31.02 Ped\\u00E1ly ve stejn\\u00E9 (nebo t\\u00E9m\\u011B\\u0159 stejn\\u00E9) \\u00FArovni\"}", null, "1.0.0" },
                    { new Guid("0a4bb26c-c374-7a9f-4227-eef18c0b49d1"), "seznam-narodni-kody-ro", "185", "{\"Name_cs\":\"185. Pouze pro \\u0159\\u00EDzen\\u00ED motorov\\u00FDch vozidel dle \\u00A7 83 odst. 5\",\"Description_cs\":\"185. Pouze pro \\u0159\\u00EDzen\\u00ED motorov\\u00FDch vozidel dle \\u00A7 83 odst. 5\"}", null, "1.0.0" },
                    { new Guid("0b508d9d-ca3d-d4a4-a7ce-a77219f26571"), "akce-ro", "akce_ro_2", "{\"Name_cs\":\"aktualizace\",\"Description_cs\":\"Aktualizace l\\u00E9ka\\u0159sk\\u00E9ho posudku.\"}", null, "1.0.0" },
                    { new Guid("0d1893c4-06ea-096a-85b6-c8d3b0f09cbf"), "druh-posudku-ro", "druh_posudku_ro_2", "{\"Name_cs\":\"roz\\u0161\\u00ED\\u0159en\\u00ED \\u0159idi\\u010Dsk\\u00E9ho opr\\u00E1vn\\u011Bn\\u00ED\",\"Description_cs\":\"Roz\\u0161\\u00ED\\u0159en\\u00ED \\u0159idi\\u010Dsk\\u00E9ho opr\\u00E1vn\\u011Bn\\u00ED.\"}", null, "1.0.0" },
                    { new Guid("12a24e45-b298-188c-b140-3f694a1d339b"), "seznam-harmonizovane-kody-ro", "40.06", "{\"Name_cs\":\"40.06 P\\u0159izp\\u016Fsoben\\u00E1 poloha volantu\",\"Description_cs\":\"40.06 P\\u0159izp\\u016Fsoben\\u00E1 poloha volantu\"}", null, "1.0.0" },
                    { new Guid("12e51624-00d6-b18b-5142-95330303c8a9"), "seznam-harmonizovane-kody-ro", "10.02", "{\"Name_cs\":\"10.02 Automatick\\u00E1 p\\u0159evodovka\",\"Description_cs\":\"10.02 Automatick\\u00E1 p\\u0159evodovka\"}", null, "1.0.0" },
                    { new Guid("169859c4-c394-a449-2074-c3e208694e9e"), "seznam-harmonizovane-kody-ro", "10.04", "{\"Name_cs\":\"10.04 P\\u0159izp\\u016Fsoben\\u00E9 \\u00FAstroj\\u00ED ovl\\u00E1d\\u00E1n\\u00ED p\\u0159evodovky\",\"Description_cs\":\"10.04 P\\u0159izp\\u016Fsoben\\u00E9 \\u00FAstroj\\u00ED ovl\\u00E1d\\u00E1n\\u00ED p\\u0159evodovky\"}", null, "1.0.0" },
                    { new Guid("1c9416e0-a5d7-f83f-b982-cfb089465ae9"), "seznam-harmonizovane-kody-ro", "03.02", "{\"Name_cs\":\"03.02 Prot\\u00E9za/ort\\u00E9za doln\\u00ED kon\\u010Detiny\",\"Description_cs\":\"03.02 Prot\\u00E9za/ort\\u00E9za doln\\u00ED kon\\u010Detiny\"}", null, "1.0.0" },
                    { new Guid("1cf6ce72-77a9-53f8-25f9-e6f0e8e63bae"), "seznam-harmonizovane-kody-ro", "40.15", "{\"Name_cs\":\"40.15 Syst\\u00E9m \\u0159\\u00EDzen\\u00ED ovl\\u00E1dan\\u00FD dv\\u011Bma rukama/pa\\u017Eemi\",\"Description_cs\":\"40.15 Syst\\u00E9m \\u0159\\u00EDzen\\u00ED ovl\\u00E1dan\\u00FD dv\\u011Bma rukama/pa\\u017Eemi\"}", null, "1.0.0" },
                    { new Guid("1e391202-f1b3-3267-2245-60e6d503b13c"), "seznam-harmonizovane-kody-ro", "40.11", "{\"Name_cs\":\"40.11 Pomocn\\u00E1 za\\u0159\\u00EDzen\\u00ED na volantu\",\"Description_cs\":\"40.11 Pomocn\\u00E1 za\\u0159\\u00EDzen\\u00ED na volantu\"}", null, "1.0.0" },
                    { new Guid("20e744ae-e46a-b56c-df42-f9ea4cfb1d8b"), "seznam-harmonizovane-kody-ro", "31.03", "{\"Name_cs\":\"31.03 Opat\\u0159en\\u00ED proti zablokov\\u00E1n\\u00ED akceler\\u00E1toru a brzd\",\"Description_cs\":\"31.03 Opat\\u0159en\\u00ED proti zablokov\\u00E1n\\u00ED akceler\\u00E1toru a brzd\"}", null, "1.0.0" },
                    { new Guid("242f4cc9-6f02-342f-5b94-23dcc0fa4595"), "seznam-harmonizovane-kody-ro", "25.01", "{\"Name_cs\":\"25.01 P\\u0159izp\\u016Fsoben\\u00FD ped\\u00E1l akceler\\u00E1toru\",\"Description_cs\":\"25.01 P\\u0159izp\\u016Fsoben\\u00FD ped\\u00E1l akceler\\u00E1toru\"}", null, "1.0.0" },
                    { new Guid("2430e460-fda4-2c71-e736-f4720f381c48"), "seznam-harmonizovane-kody-ro", "20.14", "{\"Name_cs\":\"20.14 Ovl\\u00E1d\\u00E1n\\u00ED brzdov\\u00E9ho syst\\u00E9mu s podporou vn\\u011Bj\\u0161\\u00ED s\\u00EDly\",\"Description_cs\":\"20.14 Ovl\\u00E1d\\u00E1n\\u00ED brzdov\\u00E9ho syst\\u00E9mu s podporou vn\\u011Bj\\u0161\\u00ED s\\u00EDly\"}", null, "1.0.0" },
                    { new Guid("248fb126-5d60-e8ad-ce4c-944e200e61dc"), "seznam-harmonizovane-kody-ro", "35.04", "{\"Name_cs\":\"35.04 Ovlada\\u010De ovladateln\\u00E9 pravou rukou bez pu\\u0161t\\u011Bn\\u00ED \\u0159\\u00EDzen\\u00ED\",\"Description_cs\":\"35.04 Ovlada\\u010De ovladateln\\u00E9 pravou rukou bez pu\\u0161t\\u011Bn\\u00ED \\u0159\\u00EDzen\\u00ED\"}", null, "1.0.0" },
                    { new Guid("2616145c-f6f1-10d4-884c-e18b8d0b8606"), "seznam-skupin-ro", "D1E", "{\"Name_cs\":\"skupina D1E\",\"Description_cs\":\"Skupina D1E\"}", null, "1.0.0" },
                    { new Guid("28173a63-9167-8bb2-4a65-293d5d578322"), "seznam-harmonizovane-kody-ro", "44.03", "{\"Name_cs\":\"44.03 P\\u0159izp\\u016Fsoben\\u00E1 brzda na zadn\\u00EDm kole\",\"Description_cs\":\"44.03 P\\u0159izp\\u016Fsoben\\u00E1 brzda na zadn\\u00EDm kole\"}", null, "1.0.0" },
                    { new Guid("28357728-dea9-0e43-af1f-6d99918f037c"), "stav-posudku", "stav_posudku_1", "{\"Name_cs\":\"platn\\u00FD\",\"Description_cs\":\"platn\\u00FD\"}", null, "1.0.0" },
                    { new Guid("28d8593a-4684-7233-49f7-c3ccd186dc70"), "seznam-harmonizovane-kody-ro", "44.08", "{\"Name_cs\":\"44.08 V\\u00FD\\u0161ka sedadla umo\\u017E\\u0148uj\\u00EDc\\u00ED ob\\u011B nohy na zemi\",\"Description_cs\":\"44.08 V\\u00FD\\u0161ka sedadla umo\\u017E\\u0148uj\\u00EDc\\u00ED ob\\u011B nohy na zemi\"}", null, "1.0.0" },
                    { new Guid("29763e81-5b3d-bd19-0025-d55ce323cfba"), "seznam-harmonizovane-kody-ro", "15.02", "{\"Name_cs\":\"15.02 Ru\\u010Dn\\u011B ovl\\u00E1dan\\u00E1 spojka\",\"Description_cs\":\"15.02 Ru\\u010Dn\\u011B ovl\\u00E1dan\\u00E1 spojka\"}", null, "1.0.0" },
                    { new Guid("2ba806f5-f52d-1606-72e7-2757b2899e05"), "seznam-harmonizovane-kody-ro", "15.03", "{\"Name_cs\":\"15.03 Automatick\\u00E1 spojka\",\"Description_cs\":\"15.03 Automatick\\u00E1 spojka\"}", null, "1.0.0" },
                    { new Guid("2c9f04a8-51af-45d0-48fd-bfb1dd73131b"), "seznam-harmonizovane-kody-ro", "01.01", "{\"Name_cs\":\"01.01 Br\\u00FDle\",\"Description_cs\":\"01.01 Br\\u00FDle\"}", null, "1.0.0" },
                    { new Guid("2e3017be-1f0e-cac3-1c48-a5e2974c85bf"), "seznam-narodni-kody-ro", "199", "{\"Name_cs\":\"199. Zku\\u0161ebn\\u00ED doba podm\\u00EDn\\u011Bn\\u00E9ho upu\\u0161t\\u011Bn\\u00ED od v\\u00FDkon\",\"Description_cs\":\"199. Zku\\u0161ebn\\u00ED doba podm\\u00EDn\\u011Bn\\u00E9ho upu\\u0161t\\u011Bn\\u00ED od v\\u00FDkon\"}", null, "1.0.0" },
                    { new Guid("373649f8-0351-2b65-5f0f-79d5ffeda96a"), "seznam-harmonizovane-kody-ro", "40.09", "{\"Name_cs\":\"40.09 No\\u017En\\u00ED ovl\\u00E1d\\u00E1n\\u00ED \\u0159\\u00EDzen\\u00ED\",\"Description_cs\":\"40.09 No\\u017En\\u00ED ovl\\u00E1d\\u00E1n\\u00ED \\u0159\\u00EDzen\\u00ED\"}", null, "1.0.0" },
                    { new Guid("37a769f3-e351-10e6-456e-4773bad5ccb7"), "druh-posudku-ro", "druh_posudku_ro_5", "{\"Name_cs\":\"p\\u0159ezkoum\\u00E1n\\u00ED zp\\u016Fsobilosti\",\"Description_cs\":\"P\\u0159ezkoum\\u00E1n\\u00ED zp\\u016Fsobilosti.\"}", null, "1.0.0" },
                    { new Guid("3cf21c31-2d1d-2119-bf0f-4c9e0641e75c"), "druh-posudku-ro", "druh_posudku_ro_4", "{\"Name_cs\":\"senio\\u0159i\",\"Description_cs\":\"\\u0158idi\\u010Dsk\\u00E9 opr\\u00E1vn\\u011Bn\\u00ED pro seniory.\"}", null, "1.0.0" },
                    { new Guid("3d00d3a4-a711-1f4d-e191-2fb6fab87130"), "seznam-narodni-kody-ro", "111a", "{\"Name_cs\":\"111. Nelze vykon\\u00E1vat \\u010Dinnost: a) \\u0159idi\\u010De v pracovn\\u011Bpr\\u00E1vn\\u00EDm vztahu\",\"Description_cs\":\"111. Nelze vykon\\u00E1vat \\u010Dinnost: a) \\u0159idi\\u010De v pracovn\\u011Bpr\\u00E1vn\\u00EDm vztahu\"}", null, "1.0.0" },
                    { new Guid("3da4d4e6-bf49-a17f-6c49-a636fc3aad6d"), "druh-prohlidky-ro", "druh_prohlidky_ro_1", "{\"Name_cs\":\"vstupn\\u00ED\",\"Description_cs\":\"Vstupn\\u00ED l\\u00E9ka\\u0159sk\\u00E1 prohl\\u00EDdka.\"}", null, "1.0.0" },
                    { new Guid("3ed58ee9-2a81-77de-44ea-a5a5c88d542e"), "akce-ro", "akce_ro_1", "{\"Name_cs\":\"vytvo\\u0159en\\u00ED\",\"Description_cs\":\"Vytvo\\u0159en\\u00ED l\\u00E9ka\\u0159sk\\u00E9ho posudku.\"}", null, "1.0.0" },
                    { new Guid("40c4ae0b-ffd3-71b5-4faf-44629a49219b"), "seznam-harmonizovane-kody-ro", "44.12", "{\"Name_cs\":\"44.12 P\\u0159izp\\u016Fsoben\\u00E1 \\u0159\\u00EDd\\u00EDtka\",\"Description_cs\":\"44.12 P\\u0159izp\\u016Fsoben\\u00E1 \\u0159\\u00EDd\\u00EDtka\"}", null, "1.0.0" },
                    { new Guid("4465f3d7-670e-8914-82d1-8a5af5d9e44b"), "seznam-harmonizovane-kody-ro", "01.02", "{\"Name_cs\":\"01.02 Kontaktn\\u00ED \\u010Do\\u010Dky\",\"Description_cs\":\"01.02 Kontaktn\\u00ED \\u010Do\\u010Dky\"}", null, "1.0.0" },
                    { new Guid("44af5d9b-35bf-6509-e0b8-a4e6dab4576d"), "druh-posudku-ro", "druh_posudku_ro_3", "{\"Name_cs\":\"prodlou\\u017Een\\u00ED \\u0159idi\\u010Dsk\\u00E9ho opr\\u00E1vn\\u011Bn\\u00ED\",\"Description_cs\":\"Prodlou\\u017Een\\u00ED \\u0159idi\\u010Dsk\\u00E9ho opr\\u00E1vn\\u011Bn\\u00ED.\"}", null, "1.0.0" },
                    { new Guid("491f19bb-68ca-3bbe-b288-b6a61ac004db"), "seznam-harmonizovane-kody-ro", "42.03", "{\"Name_cs\":\"42.03 P\\u0159\\u00EDdavn\\u00E9 vnit\\u0159n\\u00ED za\\u0159\\u00EDzen\\u00ED umo\\u017E\\u0148uj\\u00EDc\\u00ED v\\u00FDhled do stran\",\"Description_cs\":\"42.03 P\\u0159\\u00EDdavn\\u00E9 vnit\\u0159n\\u00ED za\\u0159\\u00EDzen\\u00ED umo\\u017E\\u0148uj\\u00EDc\\u00ED v\\u00FDhled do stran\"}", null, "1.0.0" },
                    { new Guid("4998d5f3-0003-f295-896c-b301e76ba2d9"), "seznam-harmonizovane-kody-ro", "67", "{\"Name_cs\":\"67. Z\\u00E1kaz j\\u00EDzdy na d\\u00E1lnici\",\"Description_cs\":\"67. Z\\u00E1kaz j\\u00EDzdy na d\\u00E1lnici\"}", null, "1.0.0" },
                    { new Guid("4a8f5350-3718-0c11-c063-b86f6101f7c2"), "seznam-harmonizovane-kody-ro", "62", "{\"Name_cs\":\"62. Omezen\\u00ED j\\u00EDzdy v okruhu ... km\",\"Description_cs\":\"62. Omezen\\u00ED j\\u00EDzdy v okruhu ... km\"}", null, "1.0.0" },
                    { new Guid("5038362e-2e41-d381-e46b-1f265303f746"), "seznam-harmonizovane-kody-ro", "03.01", "{\"Name_cs\":\"03.01 Prot\\u00E9za/ort\\u00E9za horn\\u00ED kon\\u010Detiny\",\"Description_cs\":\"03.01 Prot\\u00E9za/ort\\u00E9za horn\\u00ED kon\\u010Detiny\"}", null, "1.0.0" },
                    { new Guid("50bdf90c-fe83-bf6d-dd54-a9b495ca4678"), "seznam-harmonizovane-kody-ro", "42.05", "{\"Name_cs\":\"42.05 Za\\u0159\\u00EDzen\\u00ED k eliminaci mrtv\\u00E9ho \\u00FAhlu\",\"Description_cs\":\"42.05 Za\\u0159\\u00EDzen\\u00ED k eliminaci mrtv\\u00E9ho \\u00FAhlu\"}", null, "1.0.0" },
                    { new Guid("53aaeb97-342d-2c0d-ad7c-af52ecda8e2d"), "seznam-harmonizovane-kody-ro", "31.04", "{\"Name_cs\":\"31.04 Zv\\u00FD\\u0161en\\u00E1 podlaha\",\"Description_cs\":\"31.04 Zv\\u00FD\\u0161en\\u00E1 podlaha\"}", null, "1.0.0" },
                    { new Guid("555fad71-e719-2305-c322-bc13311d626c"), "seznam-harmonizovane-kody-ro", "46", "{\"Name_cs\":\"46. Pouze pro t\\u0159\\u00EDkolov\\u00E1 motorov\\u00E1 vozidla\",\"Description_cs\":\"46. Pouze pro t\\u0159\\u00EDkolov\\u00E1 motorov\\u00E1 vozidla\"}", null, "1.0.0" },
                    { new Guid("57915758-21d7-2c93-a5f0-4d6cb20987d7"), "seznam-harmonizovane-kody-ro", "15.01", "{\"Name_cs\":\"15.01 P\\u0159izp\\u016Fsoben\\u00FD ped\\u00E1l spojky\",\"Description_cs\":\"15.01 P\\u0159izp\\u016Fsoben\\u00FD ped\\u00E1l spojky\"}", null, "1.0.0" },
                    { new Guid("5a1a2957-39ee-eeb2-7915-b6ec0de7805a"), "seznam-skupin-ro", "A", "{\"Name_cs\":\"skupina A\",\"Description_cs\":\"Skupina A\"}", null, "1.0.0" },
                    { new Guid("5c79078d-1be6-4727-a1b3-e0a0e3bc475b"), "seznam-harmonizovane-kody-ro", "78", "{\"Name_cs\":\"78. Pouze pro vozidla s automatickou p\\u0159evodovkou\",\"Description_cs\":\"78. Pouze pro vozidla s automatickou p\\u0159evodovkou\"}", null, "1.0.0" },
                    { new Guid("5d59cfd1-56a1-3926-b790-54eb16d9b012"), "seznam-skupin-ro", "B", "{\"Name_cs\":\"skupina B\",\"Description_cs\":\"Skupina B\"}", null, "1.0.0" },
                    { new Guid("5d988a35-7972-1d8d-c41f-d15520fb95ed"), "seznam-harmonizovane-kody-ro", "35.02", "{\"Name_cs\":\"35.02 Ovlada\\u010De ovladateln\\u00E9 bez pu\\u0161t\\u011Bn\\u00ED \\u0159\\u00EDzen\\u00ED\",\"Description_cs\":\"35.02 Ovlada\\u010De ovladateln\\u00E9 bez pu\\u0161t\\u011Bn\\u00ED \\u0159\\u00EDzen\\u00ED\"}", null, "1.0.0" },
                    { new Guid("5e5e459f-00df-493d-42af-1c4304e8f4d7"), "seznam-skupin-ro", "T", "{\"Name_cs\":\"skupina T\",\"Description_cs\":\"Skupina T\"}", null, "1.0.0" },
                    { new Guid("5fa19222-5bce-52d7-e926-73fdfb5b2496"), "vysledek-posudku-ro", "vysledek_posudku_ro_2", "{\"Name_cs\":\"nezp\\u016Fsobil\\u00FD\",\"Description_cs\":\"Zdravotn\\u00ED nezp\\u016Fsobilost k \\u0159\\u00EDzen\\u00ED motorov\\u00FDch vozidel.\"}", null, "1.0.0" },
                    { new Guid("601aea5a-e5a1-36f8-0034-f232f1455d64"), "seznam-harmonizovane-kody-ro", "66", "{\"Name_cs\":\"66. Bez p\\u0159\\u00EDv\\u011Bsu\",\"Description_cs\":\"66. Bez p\\u0159\\u00EDv\\u011Bsu\"}", null, "1.0.0" },
                    { new Guid("64e0d66e-47fc-455f-07b7-35a9f8dc0c6d"), "seznam-skupin-ro", "BE", "{\"Name_cs\":\"skupina BE\",\"Description_cs\":\"Skupina BE\"}", null, "1.0.0" },
                    { new Guid("66561096-b005-6e09-2b24-570a3445350a"), "seznam-harmonizovane-kody-ro", "44.02", "{\"Name_cs\":\"44.02 P\\u0159izp\\u016Fsoben\\u00E1 brzda na p\\u0159edn\\u00EDm kole\",\"Description_cs\":\"44.02 P\\u0159izp\\u016Fsoben\\u00E1 brzda na p\\u0159edn\\u00EDm kole\"}", null, "1.0.0" },
                    { new Guid("67ddca50-acd2-00dc-7e87-42596c386a67"), "seznam-harmonizovane-kody-ro", "02", "{\"Name_cs\":\"02. Sluchov\\u00E9 pom\\u016Fcky/komunika\\u010Dn\\u00ED pom\\u016Fcky\",\"Description_cs\":\"02. Sluchov\\u00E9 pom\\u016Fcky/komunika\\u010Dn\\u00ED pom\\u016Fcky\"}", null, "1.0.0" },
                    { new Guid("6aa959c4-b6a9-b7a5-fc4f-74280b74ef72"), "seznam-harmonizovane-kody-ro", "25.05", "{\"Name_cs\":\"25.05 Ovl\\u00E1d\\u00E1n\\u00ED akceler\\u00E1toru kolenem\",\"Description_cs\":\"25.05 Ovl\\u00E1d\\u00E1n\\u00ED akceler\\u00E1toru kolenem\"}", null, "1.0.0" },
                    { new Guid("6cc4880e-3b11-b894-d7b2-05ba4e62bbf3"), "seznam-harmonizovane-kody-ro", "47", "{\"Name_cs\":\"47. Pouze pro vozidla s v\\u00EDce ne\\u017E dv\\u011Bma koly\",\"Description_cs\":\"47. Pouze pro vozidla s v\\u00EDce ne\\u017E dv\\u011Bma koly\"}", null, "1.0.0" },
                    { new Guid("6d8cc460-6de2-93ad-14f1-5bdf9db86fb5"), "seznam-harmonizovane-kody-ro", "15.04", "{\"Name_cs\":\"15.04 Opat\\u0159en\\u00ED proti zablokov\\u00E1n\\u00ED nebo aktivaci ped\\u00E1lu spojky\",\"Description_cs\":\"15.04 Opat\\u0159en\\u00ED proti zablokov\\u00E1n\\u00ED nebo aktivaci ped\\u00E1lu spojky\"}", null, "1.0.0" },
                    { new Guid("6e3da4c8-4f4e-0237-221c-a400a2b783d4"), "seznam-harmonizovane-kody-ro", "35.05", "{\"Name_cs\":\"35.05 Ovlada\\u010De ovladateln\\u00E9 bez pu\\u0161t\\u011Bn\\u00ED \\u0159\\u00EDzen\\u00ED a mechanismy ped\\u00E1l\\u016F\",\"Description_cs\":\"35.05 Ovlada\\u010De ovladateln\\u00E9 bez pu\\u0161t\\u011Bn\\u00ED \\u0159\\u00EDzen\\u00ED a mechanismy ped\\u00E1l\\u016F\"}", null, "1.0.0" },
                    { new Guid("75fafcec-f490-7dfc-9613-6774e04101b3"), "stav-posudku", "stav_posudku_2", "{\"Name_cs\":\"neplatn\\u00FD\",\"Description_cs\":\"neplatn\\u00FD\"}", null, "1.0.0" },
                    { new Guid("772b6c4c-533f-aad0-ead5-2dfe04cd1b99"), "seznam-narodni-kody-ro", "160", "{\"Name_cs\":\"160. V\\u00FDjimka z v\\u011Bku u \\u0159idi\\u010Dsk\\u00E9ho opr\\u00E1vn\\u011Bn\\u00ED pro sportovn\\u00ED sout\\u011B\\u017E\",\"Description_cs\":\"160. V\\u00FDjimka z v\\u011Bku u \\u0159idi\\u010Dsk\\u00E9ho opr\\u00E1vn\\u011Bn\\u00ED pro sportovn\\u00ED sout\\u011B\\u017E\"}", null, "1.0.0" },
                    { new Guid("78136f4f-95b0-3dcb-aab7-1e5fddd56d58"), "seznam-harmonizovane-kody-ro", "20.01", "{\"Name_cs\":\"20.01 P\\u0159izp\\u016Fsoben\\u00FD brzdov\\u00FD ped\\u00E1l\",\"Description_cs\":\"20.01 P\\u0159izp\\u016Fsoben\\u00FD brzdov\\u00FD ped\\u00E1l\"}", null, "1.0.0" },
                    { new Guid("7b802917-d70f-3fb3-b5fb-5c08be70a7f2"), "seznam-harmonizovane-kody-ro", "40.05", "{\"Name_cs\":\"40.05 P\\u0159izp\\u016Fsoben\\u00FD volant\",\"Description_cs\":\"40.05 P\\u0159izp\\u016Fsoben\\u00FD volant\"}", null, "1.0.0" },
                    { new Guid("7bbc132b-706c-a70c-f095-b54b444882a6"), "seznam-harmonizovane-kody-ro", "20.07", "{\"Name_cs\":\"20.07 Ovl\\u00E1d\\u00E1n\\u00ED brzdy s pou\\u017Eit\\u00EDm maxim\\u00E1ln\\u00ED s\\u00EDly ... N\",\"Description_cs\":\"20.07 Ovl\\u00E1d\\u00E1n\\u00ED brzdy s pou\\u017Eit\\u00EDm maxim\\u00E1ln\\u00ED s\\u00EDly ... N\"}", null, "1.0.0" },
                    { new Guid("7ca6b2fd-1c0f-2d02-8858-71b7b02e35fe"), "seznam-harmonizovane-kody-ro", "20.09", "{\"Name_cs\":\"20.09 P\\u0159izp\\u016Fsoben\\u00E1 parkovac\\u00ED brzda\",\"Description_cs\":\"20.09 P\\u0159izp\\u016Fsoben\\u00E1 parkovac\\u00ED brzda\"}", null, "1.0.0" },
                    { new Guid("7da53a76-2fb9-2a94-60b3-fae4111ab994"), "seznam-harmonizovane-kody-ro", "33.02", "{\"Name_cs\":\"33.02 Akceler\\u00E1tor, brzda a \\u0159\\u00EDzen\\u00ED ovl\\u00E1dan\\u00E9 ob\\u011Bma rukama\",\"Description_cs\":\"33.02 Akceler\\u00E1tor, brzda a \\u0159\\u00EDzen\\u00ED ovl\\u00E1dan\\u00E9 ob\\u011Bma rukama\"}", null, "1.0.0" },
                    { new Guid("7ed5caa2-08d4-6cbe-f6e4-a94bc979a6a6"), "seznam-harmonizovane-kody-ro", "33.01", "{\"Name_cs\":\"33.01 Akceler\\u00E1tor, brzda a \\u0159\\u00EDzen\\u00ED ovl\\u00E1dan\\u00E9 jednou rukou\",\"Description_cs\":\"33.01 Akceler\\u00E1tor, brzda a \\u0159\\u00EDzen\\u00ED ovl\\u00E1dan\\u00E9 jednou rukou\"}", null, "1.0.0" },
                    { new Guid("7f209fc9-b639-68b9-fce0-c4d0951f0246"), "seznam-harmonizovane-kody-ro", "32.01", "{\"Name_cs\":\"32.01 Akceler\\u00E1tor a provozn\\u00ED brzda jako syst\\u00E9m ovl\\u00E1dan\\u00FD jednou rukou\",\"Description_cs\":\"32.01 Akceler\\u00E1tor a provozn\\u00ED brzda jako syst\\u00E9m ovl\\u00E1dan\\u00FD jednou rukou\"}", null, "1.0.0" },
                    { new Guid("7fa19dba-9062-b0e4-5029-2481a6d046d1"), "seznam-narodni-kody-ro", "111c", "{\"Name_cs\":\"111. Nelze vykon\\u00E1vat \\u010Dinnost: c) u\\u010Ditele v\\u00FDcviku v \\u0159\\u00EDzen\\u00ED\",\"Description_cs\":\"111. Nelze vykon\\u00E1vat \\u010Dinnost: c) u\\u010Ditele v\\u00FDcviku v \\u0159\\u00EDzen\\u00ED\"}", null, "1.0.0" },
                    { new Guid("805e2f7f-da02-fe77-d387-31cd970fbe73"), "vysledek-posudku-ro", "vysledek_posudku_ro_3", "{\"Name_cs\":\"zp\\u016Fsobil\\u00FD s podm\\u00EDnkou\",\"Description_cs\":\"Zdravotn\\u00ED zp\\u016Fsobilost k \\u0159\\u00EDzen\\u00ED motorov\\u00FDch vozidel s podm\\u00EDnkou.\"}", null, "1.0.0" },
                    { new Guid("811ecad5-2cb9-2945-77bb-f9c1836155b9"), "akce-ro", "akce_ro_3", "{\"Name_cs\":\"zneplatn\\u011Bn\\u00ED\",\"Description_cs\":\"Zneplatn\\u011Bn\\u00ED l\\u00E9ka\\u0159sk\\u00E9ho posudku.\"}", null, "1.0.0" },
                    { new Guid("83cf2f42-558a-c583-2b0b-b67610d3a16c"), "seznam-harmonizovane-kody-ro", "44.01", "{\"Name_cs\":\"44.01 Samostatn\\u011B ovl\\u00E1dan\\u00E1 brzda\",\"Description_cs\":\"44.01 Samostatn\\u011B ovl\\u00E1dan\\u00E1 brzda\"}", null, "1.0.0" },
                    { new Guid("83f35470-5345-e566-c318-3ca9b226cac7"), "druh-prohlidky-ro", "druh_prohlidky_ro_3", "{\"Name_cs\":\"mimo\\u0159\\u00E1dn\\u00E1\",\"Description_cs\":\"Mimo\\u0159\\u00E1dn\\u00E1 l\\u00E9ka\\u0159sk\\u00E1 prohl\\u00EDdka.\"}", null, "1.0.0" },
                    { new Guid("8889245e-a72b-768e-451a-635a82ed9a2a"), "seznam-skupin-ro", "C", "{\"Name_cs\":\"skupina C\",\"Description_cs\":\"Skupina C\"}", null, "1.0.0" },
                    { new Guid("8995b7a3-8595-8198-cbf8-5f24361b22ce"), "seznam-harmonizovane-kody-ro", "25.06", "{\"Name_cs\":\"25.06 Ovl\\u00E1d\\u00E1n\\u00ED akceler\\u00E1toru s podporou vn\\u011Bj\\u0161\\u00ED s\\u00EDly\",\"Description_cs\":\"25.06 Ovl\\u00E1d\\u00E1n\\u00ED akceler\\u00E1toru s podporou vn\\u011Bj\\u0161\\u00ED s\\u00EDly\"}", null, "1.0.0" },
                    { new Guid("89b93d8e-8c5d-cf54-e206-186bae457e12"), "seznam-harmonizovane-kody-ro", "64", "{\"Name_cs\":\"64. J\\u00EDzda rychlost\\u00ED nep\\u0159esahuj\\u00EDc\\u00ED ... km/h\",\"Description_cs\":\"64. J\\u00EDzda rychlost\\u00ED nep\\u0159esahuj\\u00EDc\\u00ED ... km/h\"}", null, "1.0.0" },
                    { new Guid("8d2d2b69-4ba0-3a8a-fba6-bd340241a1a2"), "seznam-harmonizovane-kody-ro", "65", "{\"Name_cs\":\"65. \\u0158\\u00EDzen\\u00ED vozidla povoleno v\\u00FDhradn\\u011B v doprovodu\",\"Description_cs\":\"65. \\u0158\\u00EDzen\\u00ED vozidla povoleno v\\u00FDhradn\\u011B v doprovodu\"}", null, "1.0.0" },
                    { new Guid("8f91c15d-d901-3a7e-5568-3a2cffee2878"), "seznam-harmonizovane-kody-ro", "40.01", "{\"Name_cs\":\"40.01 \\u0158\\u00EDzen\\u00ED s pou\\u017Eit\\u00EDm maxim\\u00E1ln\\u00ED ovl\\u00E1dac\\u00ED s\\u00EDly\",\"Description_cs\":\"40.01 \\u0158\\u00EDzen\\u00ED s pou\\u017Eit\\u00EDm maxim\\u00E1ln\\u00ED ovl\\u00E1dac\\u00ED s\\u00EDly\"}", null, "1.0.0" },
                    { new Guid("8ff271d7-01b2-1c3b-d4c7-116d0fc977cb"), "seznam-harmonizovane-kody-ro", "35.03", "{\"Name_cs\":\"35.03 Ovlada\\u010De ovladateln\\u00E9 levou rukou bez pu\\u0161t\\u011Bn\\u00ED \\u0159\\u00EDzen\\u00ED\",\"Description_cs\":\"35.03 Ovlada\\u010De ovladateln\\u00E9 levou rukou bez pu\\u0161t\\u011Bn\\u00ED \\u0159\\u00EDzen\\u00ED\"}", null, "1.0.0" },
                    { new Guid("95ea4d09-5912-5547-7c56-f6052a5c9a36"), "seznam-narodni-kody-ro", "111b", "{\"Name_cs\":\"111. Nelze vykon\\u00E1vat \\u010Dinnost: b) \\u0159idi\\u010De OSV\\u010C\",\"Description_cs\":\"111. Nelze vykon\\u00E1vat \\u010Dinnost: b) \\u0159idi\\u010De OSV\\u010C\"}", null, "1.0.0" },
                    { new Guid("9646e9ed-d6fa-c2cd-e6f3-abedfc1615c0"), "seznam-narodni-kody-ro", "105", "{\"Name_cs\":\"105. Jin\\u00E1 zdravotn\\u00ED omezen\\u00ED\",\"Description_cs\":\"105. Jin\\u00E1 zdravotn\\u00ED omezen\\u00ED, kter\\u00E1 nejsou uvedena v \\u010D\\u00E1sti I t\\u00E9to p\\u0159\\u00EDlohy\"}", null, "1.0.0" },
                    { new Guid("981b2ea8-d5cc-46bf-3c00-df8c1d5d3d35"), "seznam-skupin-ro", "A1", "{\"Name_cs\":\"skupina A1\",\"Description_cs\":\"Skupina A1\"}", null, "1.0.0" },
                    { new Guid("9a0be359-195a-9891-e0fc-79cbe655d08d"), "vysledek-posudku-ro", "vysledek_posudku_ro_1", "{\"Name_cs\":\"zp\\u016Fsobil\\u00FD\",\"Description_cs\":\"Zdravotn\\u00ED zp\\u016Fsobilost k \\u0159\\u00EDzen\\u00ED motorov\\u00FDch vozidel.\"}", null, "1.0.0" },
                    { new Guid("9bfd928e-6859-c19b-e92d-6ed79f46bdbb"), "seznam-skupin-ro", "D", "{\"Name_cs\":\"skupina D\",\"Description_cs\":\"Skupina D\"}", null, "1.0.0" },
                    { new Guid("9e1f7442-7886-7772-f3ab-4ecae124a9e0"), "seznam-harmonizovane-kody-ro", "20.13", "{\"Name_cs\":\"20.13 Ovl\\u00E1d\\u00E1n\\u00ED brzdy kolenem\",\"Description_cs\":\"20.13 Ovl\\u00E1d\\u00E1n\\u00ED brzdy kolenem\"}", null, "1.0.0" },
                    { new Guid("a0638854-c6d5-3e40-aa43-741a6b3c1665"), "seznam-narodni-kody-ro", "115", "{\"Name_cs\":\"115. Posilova\\u010D spojky\",\"Description_cs\":\"115. Posilova\\u010D spojky\"}", null, "1.0.0" },
                    { new Guid("a1e3df9b-60f2-13bc-d397-9d40f031336a"), "seznam-harmonizovane-kody-ro", "44.04", "{\"Name_cs\":\"44.04 P\\u0159izp\\u016Fsoben\\u00E1 rukoje\\u0165 akceler\\u00E1toru\",\"Description_cs\":\"44.04 P\\u0159izp\\u016Fsoben\\u00E1 rukoje\\u0165 akceler\\u00E1toru\"}", null, "1.0.0" },
                    { new Guid("a8193c87-aa77-c110-90d2-493fc189f8bd"), "seznam-harmonizovane-kody-ro", "20.06", "{\"Name_cs\":\"20.06 Ru\\u010Dn\\u011B ovl\\u00E1dan\\u00E1 provozn\\u00ED brzda\",\"Description_cs\":\"20.06 Ru\\u010Dn\\u011B ovl\\u00E1dan\\u00E1 provozn\\u00ED brzda\"}", null, "1.0.0" },
                    { new Guid("aadcf2e5-1db7-2d4d-f8c8-28795a53599e"), "seznam-harmonizovane-kody-ro", "25.08", "{\"Name_cs\":\"25.08 Ped\\u00E1l akceler\\u00E1toru nalevo\",\"Description_cs\":\"25.08 Ped\\u00E1l akceler\\u00E1toru nalevo\"}", null, "1.0.0" },
                    { new Guid("abbf94be-17bc-f260-2e1f-f84252f4d6d7"), "seznam-harmonizovane-kody-ro", "63", "{\"Name_cs\":\"63. \\u0158\\u00EDzen\\u00ED vozidla bez cestuj\\u00EDc\\u00EDch\",\"Description_cs\":\"63. \\u0158\\u00EDzen\\u00ED vozidla bez cestuj\\u00EDc\\u00EDch\"}", null, "1.0.0" },
                    { new Guid("b1fe7fd7-bae1-0eb4-4456-c13b88e06a79"), "seznam-harmonizovane-kody-ro", "25.09", "{\"Name_cs\":\"25.09 Opat\\u0159en\\u00ED proti zablokov\\u00E1n\\u00ED nebo aktivaci akceler\\u00E1toru\",\"Description_cs\":\"25.09 Opat\\u0159en\\u00ED proti zablokov\\u00E1n\\u00ED nebo aktivaci akceler\\u00E1toru\"}", null, "1.0.0" },
                    { new Guid("b4b1b365-4b0b-1c60-4b86-fd1e3506e499"), "seznam-harmonizovane-kody-ro", "43.07", "{\"Name_cs\":\"43.07 Typ bezpe\\u010Dnostn\\u00EDho p\\u00E1su s podporou pro dobrou stabilitu\",\"Description_cs\":\"43.07 Typ bezpe\\u010Dnostn\\u00EDho p\\u00E1su s podporou pro dobrou stabilitu\"}", null, "1.0.0" },
                    { new Guid("b932aecd-8f73-d5f8-bd82-87114a387d92"), "seznam-skupin-ro", "DE", "{\"Name_cs\":\"skupina DE\",\"Description_cs\":\"Skupina DE\"}", null, "1.0.0" },
                    { new Guid("b9af1a94-ce5a-8914-f790-7f17dee89ca7"), "seznam-harmonizovane-kody-ro", "43.01", "{\"Name_cs\":\"43.01 V\\u00FD\\u0161ka sedadla \\u0159idi\\u010De umo\\u017E\\u0148uj\\u00EDc\\u00ED norm\\u00E1ln\\u00ED v\\u00FDhled\",\"Description_cs\":\"43.01 V\\u00FD\\u0161ka sedadla \\u0159idi\\u010De umo\\u017E\\u0148uj\\u00EDc\\u00ED norm\\u00E1ln\\u00ED v\\u00FDhled\"}", null, "1.0.0" },
                    { new Guid("bf72d914-e561-1e18-68b3-53f4692a0ae7"), "seznam-harmonizovane-kody-ro", "32.02", "{\"Name_cs\":\"32.02 Akceler\\u00E1tor a provozn\\u00ED brzda jako syst\\u00E9m ovl\\u00E1dan\\u00FD vn\\u011Bj\\u0161\\u00ED silou\",\"Description_cs\":\"32.02 Akceler\\u00E1tor a provozn\\u00ED brzda jako syst\\u00E9m ovl\\u00E1dan\\u00FD vn\\u011Bj\\u0161\\u00ED silou\"}", null, "1.0.0" },
                    { new Guid("c3cd493b-7894-feac-58d7-42ee4423950e"), "skupina-zadatel-ridic-ro", "skupina_ro_1", "{\"Name_cs\":\"skupina 1\",\"Description_cs\":\"\\u017Dadatel\\u00E9 a dr\\u017Eitel\\u00E9 \\u0159idi\\u010Dsk\\u00FDch opr\\u00E1vn\\u011Bn\\u00ED definovan\\u00FDch vyhl\\u00E1\\u0161kou 277/2004 Sb.\"}", null, "1.0.0" },
                    { new Guid("c40df9e2-3558-52df-375f-8665be57b289"), "seznam-harmonizovane-kody-ro", "20.03", "{\"Name_cs\":\"20.03 Brzdov\\u00FD ped\\u00E1l upraven\\u00FD na levou nohu\",\"Description_cs\":\"20.03 Brzdov\\u00FD ped\\u00E1l upraven\\u00FD na levou nohu\"}", null, "1.0.0" },
                    { new Guid("c5d855d5-b70f-df14-178e-f1f4fcd4505e"), "skupina-zadatel-ridic-ro", "skupina_ro_2", "{\"Name_cs\":\"skupina 2\",\"Description_cs\":\"\\u017Dadatel\\u00E9 a dr\\u017Eitel\\u00E9 \\u0159idi\\u010Dsk\\u00FDch opr\\u00E1vn\\u011Bn\\u00ED definovan\\u00FDch vyhl\\u00E1\\u0161kou 277/2004 Sb.\"}", null, "1.0.0" },
                    { new Guid("c733bd7d-64d2-7944-ca57-261a00b6c930"), "seznam-harmonizovane-kody-ro", "20.12", "{\"Name_cs\":\"20.12 Opat\\u0159en\\u00ED proti zablokov\\u00E1n\\u00ED nebo aktivaci brzdov\\u00E9ho ped\\u00E1lu\",\"Description_cs\":\"20.12 Opat\\u0159en\\u00ED proti zablokov\\u00E1n\\u00ED nebo aktivaci brzdov\\u00E9ho ped\\u00E1lu\"}", null, "1.0.0" },
                    { new Guid("cc327d47-a2d7-8de0-7577-299cb4bcb1b3"), "seznam-harmonizovane-kody-ro", "25.04", "{\"Name_cs\":\"25.04 Ru\\u010Dn\\u011B ovl\\u00E1dan\\u00FD akceler\\u00E1tor\",\"Description_cs\":\"25.04 Ru\\u010Dn\\u011B ovl\\u00E1dan\\u00FD akceler\\u00E1tor\"}", null, "1.0.0" },
                    { new Guid("ce786369-a74f-269e-5d13-89d7ef9af150"), "seznam-harmonizovane-kody-ro", "44.09", "{\"Name_cs\":\"44.09 Maxim\\u00E1ln\\u00ED ovl\\u00E1dac\\u00ED s\\u00EDla brzdy p\\u0159edn\\u00EDho kola\",\"Description_cs\":\"44.09 Maxim\\u00E1ln\\u00ED ovl\\u00E1dac\\u00ED s\\u00EDla brzdy p\\u0159edn\\u00EDho kola\"}", null, "1.0.0" },
                    { new Guid("d640bcdd-dbe0-8305-7733-a6aa07fea4d1"), "seznam-harmonizovane-kody-ro", "61", "{\"Name_cs\":\"61. Omezen\\u00ED j\\u00EDzdy podle denn\\u00ED doby\",\"Description_cs\":\"61. Omezen\\u00ED j\\u00EDzdy podle denn\\u00ED doby\"}", null, "1.0.0" },
                    { new Guid("d7e754bf-f9b8-8318-eefc-ec1bd9e69c14"), "seznam-harmonizovane-kody-ro", "01.06", "{\"Name_cs\":\"01.06 Br\\u00FDle nebo kontaktn\\u00ED \\u010Do\\u010Dky\",\"Description_cs\":\"01.06 Br\\u00FDle nebo kontaktn\\u00ED \\u010Do\\u010Dky\"}", null, "1.0.0" },
                    { new Guid("d7f010b1-4829-43f1-b764-8b5b29fda23c"), "seznam-harmonizovane-kody-ro", "44.11", "{\"Name_cs\":\"44.11 P\\u0159izp\\u016Fsoben\\u00E1 stupa\\u010Dka\",\"Description_cs\":\"44.11 P\\u0159izp\\u016Fsoben\\u00E1 stupa\\u010Dka\"}", null, "1.0.0" },
                    { new Guid("d9dba26a-acb3-e438-6f7b-12fcf9650027"), "seznam-narodni-kody-ro", "172", "{\"Name_cs\":\"172. Omezen\\u00ED skupiny A pouze k \\u0159\\u00EDzen\\u00ED motorov\\u00E9ho voz\\u00EDku pro invalidy\",\"Description_cs\":\"172. Omezen\\u00ED skupiny A pouze k \\u0159\\u00EDzen\\u00ED motorov\\u00E9ho voz\\u00EDku pro invalidy\"}", null, "1.0.0" },
                    { new Guid("dfd0054d-48a7-608d-91b3-73ae482a8a91"), "seznam-harmonizovane-kody-ro", "40.14", "{\"Name_cs\":\"40.14 Syst\\u00E9m \\u0159\\u00EDzen\\u00ED ovl\\u00E1dan\\u00FD jednou rukou/pa\\u017E\\u00ED\",\"Description_cs\":\"40.14 Syst\\u00E9m \\u0159\\u00EDzen\\u00ED ovl\\u00E1dan\\u00FD jednou rukou/pa\\u017E\\u00ED\"}", null, "1.0.0" },
                    { new Guid("dffef6e9-0aa7-7a49-67d4-42d2a0bafcfc"), "seznam-harmonizovane-kody-ro", "43.02", "{\"Name_cs\":\"43.02 Anatomicky p\\u0159izp\\u016Fsoben\\u00E9 sedadlo \\u0159idi\\u010De\",\"Description_cs\":\"43.02 Anatomicky p\\u0159izp\\u016Fsoben\\u00E9 sedadlo \\u0159idi\\u010De\"}", null, "1.0.0" },
                    { new Guid("e0e3a15a-a7fe-340b-7164-a4c311ce1446"), "seznam-skupin-ro", "AM", "{\"Name_cs\":\"skupina AM\",\"Description_cs\":\"Skupina AM\"}", null, "1.0.0" },
                    { new Guid("e14e9e44-f8fb-ab37-f83e-bd5979d40cf5"), "seznam-harmonizovane-kody-ro", "43.03", "{\"Name_cs\":\"43.03 Sedadlo \\u0159idi\\u010De s postrann\\u00ED op\\u011Brou\",\"Description_cs\":\"43.03 Sedadlo \\u0159idi\\u010De s postrann\\u00ED op\\u011Brou\"}", null, "1.0.0" },
                    { new Guid("e1c8a801-962a-c1a0-e0ce-fa846e86e6fd"), "seznam-harmonizovane-kody-ro", "44.10", "{\"Name_cs\":\"44.10 Maxim\\u00E1ln\\u00ED ovl\\u00E1dac\\u00ED s\\u00EDla brzdy zadn\\u00EDho kola\",\"Description_cs\":\"44.10 Maxim\\u00E1ln\\u00ED ovl\\u00E1dac\\u00ED s\\u00EDla brzdy zadn\\u00EDho kola\"}", null, "1.0.0" },
                    { new Guid("e259294e-6d8d-168b-9597-85079a216f39"), "seznam-harmonizovane-kody-ro", "45", "{\"Name_cs\":\"45. Motocykl pouze s postrann\\u00EDm voz\\u00EDkem\",\"Description_cs\":\"45. Motocykl pouze s postrann\\u00EDm voz\\u00EDkem\"}", null, "1.0.0" },
                    { new Guid("e76012a2-89b2-964b-ed90-a739ec483048"), "druh-posudku-ro", "druh_posudku_ro_1", "{\"Name_cs\":\"prvo\\u0159idi\\u010D\",\"Description_cs\":\"Vyd\\u00E1n\\u00ED \\u0159idi\\u010Dsk\\u00E9ho opr\\u00E1vn\\u011Bn\\u00ED (prvo\\u0159idi\\u010D).\"}", null, "1.0.0" },
                    { new Guid("e8f9096d-304a-2bb8-4eb3-83b3c524ac25"), "seznam-harmonizovane-kody-ro", "42.01", "{\"Name_cs\":\"42.01 P\\u0159izp\\u016Fsoben\\u00E9 za\\u0159\\u00EDzen\\u00ED pro v\\u00FDhled dozadu\",\"Description_cs\":\"42.01 P\\u0159izp\\u016Fsoben\\u00E9 za\\u0159\\u00EDzen\\u00ED pro v\\u00FDhled dozadu\"}", null, "1.0.0" },
                    { new Guid("ed94e604-4294-1a61-2373-cbf82a138636"), "seznam-harmonizovane-kody-ro", "01.07", "{\"Name_cs\":\"01.07 Specifick\\u00E9 optick\\u00E9 pom\\u016Fcky\",\"Description_cs\":\"01.07 Specifick\\u00E9 optick\\u00E9 pom\\u016Fcky\"}", null, "1.0.0" },
                    { new Guid("ee41765c-e68a-7f70-7a70-f6a9716c2298"), "seznam-skupin-ro", "C1", "{\"Name_cs\":\"skupina C1\",\"Description_cs\":\"Skupina C1\"}", null, "1.0.0" },
                    { new Guid("ee576c08-9021-365d-64f4-fa77f4d27332"), "seznam-harmonizovane-kody-ro", "25.03", "{\"Name_cs\":\"25.03 Sklopen\\u00FD ped\\u00E1l akceler\\u00E1toru\",\"Description_cs\":\"25.03 Sklopen\\u00FD ped\\u00E1l akceler\\u00E1toru\"}", null, "1.0.0" },
                    { new Guid("eec659a7-c008-9b34-78cb-beece23d7b60"), "seznam-narodni-kody-ro", "175", "{\"Name_cs\":\"175. Omezen\\u00ED skupiny D pouze k \\u0159\\u00EDzen\\u00ED vozidla MHD\",\"Description_cs\":\"175. Omezen\\u00ED skupiny D pouze k \\u0159\\u00EDzen\\u00ED vozidla MHD\"}", null, "1.0.0" },
                    { new Guid("ef67c555-c15f-79fa-0de4-9415b1424bd6"), "seznam-harmonizovane-kody-ro", "50", "{\"Name_cs\":\"50. Omezen\\u00ED na ur\\u010Dit\\u00E9 vozidlo/\\u010D\\u00EDslo podvozku (VIN)\",\"Description_cs\":\"50. Omezen\\u00ED na ur\\u010Dit\\u00E9 vozidlo/\\u010D\\u00EDslo podvozku (VIN)\"}", null, "1.0.0" },
                    { new Guid("efd8ee29-7ff1-526c-aa00-c398c82e8603"), "seznam-harmonizovane-kody-ro", "43.06", "{\"Name_cs\":\"43.06 P\\u0159izp\\u016Fsoben\\u00ED bezpe\\u010Dnostn\\u00EDho p\\u00E1su\",\"Description_cs\":\"43.06 P\\u0159izp\\u016Fsoben\\u00ED bezpe\\u010Dnostn\\u00EDho p\\u00E1su\"}", null, "1.0.0" },
                    { new Guid("f018bd1b-4ddb-2329-f8ab-b75060d2b7ec"), "seznam-skupin-ro", "C1E", "{\"Name_cs\":\"skupina C1E\",\"Description_cs\":\"Skupina C1E\"}", null, "1.0.0" },
                    { new Guid("f05817f2-4c5a-6821-1f51-e63acfb01f00"), "seznam-harmonizovane-kody-ro", "43.04", "{\"Name_cs\":\"43.04 Sedadlo \\u0159idi\\u010De s op\\u011Brkou ruky\",\"Description_cs\":\"43.04 Sedadlo \\u0159idi\\u010De s op\\u011Brkou ruky\"}", null, "1.0.0" },
                    { new Guid("f0b4895a-f21b-da47-afa5-b470d4dbe44d"), "seznam-harmonizovane-kody-ro", "20.04", "{\"Name_cs\":\"20.04 Posuvn\\u00FD brzdov\\u00FD ped\\u00E1l\",\"Description_cs\":\"20.04 Posuvn\\u00FD brzdov\\u00FD ped\\u00E1l\"}", null, "1.0.0" },
                    { new Guid("f92132ad-2f67-d7ad-9097-6679a51b40c6"), "seznam-skupin-ro", "D1", "{\"Name_cs\":\"skupina D1\",\"Description_cs\":\"Skupina D1\"}", null, "1.0.0" },
                    { new Guid("f9ee1a56-b74a-2c4d-9fff-abb91688f592"), "seznam-skupin-ro", "B1", "{\"Name_cs\":\"skupina B1\",\"Description_cs\":\"Skupina B1\"}", null, "1.0.0" },
                    { new Guid("fb53866d-deb6-bc2d-1a19-4b4e427ac174"), "seznam-harmonizovane-kody-ro", "20.05", "{\"Name_cs\":\"20.05 Sklopen\\u00FD brzdov\\u00FD ped\\u00E1l\",\"Description_cs\":\"20.05 Sklopen\\u00FD brzdov\\u00FD ped\\u00E1l\"}", null, "1.0.0" },
                    { new Guid("fdf359bc-7272-e8b4-d69a-7c06fca4924c"), "druh-prohlidky-ro", "druh_prohlidky_ro_2", "{\"Name_cs\":\"pravideln\\u00E1\",\"Description_cs\":\"Pravideln\\u00E1 l\\u00E9ka\\u0159sk\\u00E1 prohl\\u00EDdka.\"}", null, "1.0.0" },
                    { new Guid("ff34a2bb-7df2-6610-759d-7e3b9a800718"), "seznam-skupin-ro", "A2", "{\"Name_cs\":\"skupina A2\",\"Description_cs\":\"Skupina A2\"}", null, "1.0.0" }
                });

            migrationBuilder.InsertData(
                table: "Ciselniky",
                columns: new[] { "Id", "Kod", "PlatnostDo", "PlatnostOd", "PrekladyJson", "Verze" },
                values: new object[,]
                {
                    { new Guid("147cf25b-76ae-16a7-170e-63783ef2c54e"), "seznam-harmonizovane-kody-ro", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "{\"Description_en\":\"Harmonised codes for the restriction of Mas\"}", "1.0.0" },
                    { new Guid("447f5c6c-7a98-350e-4de5-2147662ef433"), "seznam-narodni-kody-ro", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "{\"Description_en\":\"National codes for MA restrictions\"}", "1.0.0" },
                    { new Guid("4c78e141-6928-8ef6-2e00-d46969dd606e"), "druh-posudku-ro", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "{\"Description_en\":\"Type of report for the MA\"}", "1.0.0" },
                    { new Guid("55915f34-7205-32e7-7058-bc74a0903b84"), "druh-prohlidky-ro", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "{\"Description_en\":\"Type of medical examination performed\"}", "1.0.0" },
                    { new Guid("5907781a-9d4e-3f79-94fa-6ab75eccf6bf"), "skupina-zadatel-ridic-ro", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "{\"Description_en\":\"Group of applicants, holders of driving licences\"}", "1.0.0" },
                    { new Guid("7942c0f1-6a00-a2e7-30bb-d3b33fa2727b"), "stav-posudku", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "{\"Description_en\":\"Medical Opinion Status\"}", "1.0.0" },
                    { new Guid("9052b182-f12b-60ff-1f40-9bae43883e7b"), "vysledek-posudku-ro", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "{\"Description_en\":\"Result of the report for the MA\"}", "1.0.0" },
                    { new Guid("b21af6d9-b28a-98c8-ee88-818593a2fbe2"), "seznam-skupin-ro", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "{\"Description_en\":\"Driving License Group\"}", "1.0.0" },
                    { new Guid("c8be1992-0320-35db-5dbd-0262acebb01c"), "akce-ro", null, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "{\"Description_en\":\"Action type\"}", "1.0.0" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("00c40302-fe85-d87f-52b3-856b3d982d2a"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("023cbbe1-edd7-1745-6da0-699432ecc9b3"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("0500d97e-4c22-6a25-0eec-3ad3fac4b757"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("06a40357-20dc-6b72-be4f-6473ad0f8d15"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("06b4db25-d5f4-c7d5-22d6-cfaba8d77d6b"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("0a4bb26c-c374-7a9f-4227-eef18c0b49d1"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("0b508d9d-ca3d-d4a4-a7ce-a77219f26571"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("0d1893c4-06ea-096a-85b6-c8d3b0f09cbf"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("12a24e45-b298-188c-b140-3f694a1d339b"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("12e51624-00d6-b18b-5142-95330303c8a9"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("169859c4-c394-a449-2074-c3e208694e9e"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("1c9416e0-a5d7-f83f-b982-cfb089465ae9"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("1cf6ce72-77a9-53f8-25f9-e6f0e8e63bae"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("1e391202-f1b3-3267-2245-60e6d503b13c"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("20e744ae-e46a-b56c-df42-f9ea4cfb1d8b"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("242f4cc9-6f02-342f-5b94-23dcc0fa4595"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("2430e460-fda4-2c71-e736-f4720f381c48"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("248fb126-5d60-e8ad-ce4c-944e200e61dc"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("2616145c-f6f1-10d4-884c-e18b8d0b8606"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("28173a63-9167-8bb2-4a65-293d5d578322"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("28357728-dea9-0e43-af1f-6d99918f037c"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("28d8593a-4684-7233-49f7-c3ccd186dc70"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("29763e81-5b3d-bd19-0025-d55ce323cfba"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("2ba806f5-f52d-1606-72e7-2757b2899e05"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("2c9f04a8-51af-45d0-48fd-bfb1dd73131b"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("2e3017be-1f0e-cac3-1c48-a5e2974c85bf"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("373649f8-0351-2b65-5f0f-79d5ffeda96a"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("37a769f3-e351-10e6-456e-4773bad5ccb7"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("3cf21c31-2d1d-2119-bf0f-4c9e0641e75c"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("3d00d3a4-a711-1f4d-e191-2fb6fab87130"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("3da4d4e6-bf49-a17f-6c49-a636fc3aad6d"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("3ed58ee9-2a81-77de-44ea-a5a5c88d542e"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("40c4ae0b-ffd3-71b5-4faf-44629a49219b"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("4465f3d7-670e-8914-82d1-8a5af5d9e44b"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("44af5d9b-35bf-6509-e0b8-a4e6dab4576d"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("491f19bb-68ca-3bbe-b288-b6a61ac004db"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("4998d5f3-0003-f295-896c-b301e76ba2d9"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("4a8f5350-3718-0c11-c063-b86f6101f7c2"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("5038362e-2e41-d381-e46b-1f265303f746"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("50bdf90c-fe83-bf6d-dd54-a9b495ca4678"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("53aaeb97-342d-2c0d-ad7c-af52ecda8e2d"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("555fad71-e719-2305-c322-bc13311d626c"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("57915758-21d7-2c93-a5f0-4d6cb20987d7"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("5a1a2957-39ee-eeb2-7915-b6ec0de7805a"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("5c79078d-1be6-4727-a1b3-e0a0e3bc475b"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("5d59cfd1-56a1-3926-b790-54eb16d9b012"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("5d988a35-7972-1d8d-c41f-d15520fb95ed"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("5e5e459f-00df-493d-42af-1c4304e8f4d7"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("5fa19222-5bce-52d7-e926-73fdfb5b2496"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("601aea5a-e5a1-36f8-0034-f232f1455d64"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("64e0d66e-47fc-455f-07b7-35a9f8dc0c6d"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("66561096-b005-6e09-2b24-570a3445350a"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("67ddca50-acd2-00dc-7e87-42596c386a67"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("6aa959c4-b6a9-b7a5-fc4f-74280b74ef72"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("6cc4880e-3b11-b894-d7b2-05ba4e62bbf3"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("6d8cc460-6de2-93ad-14f1-5bdf9db86fb5"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("6e3da4c8-4f4e-0237-221c-a400a2b783d4"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("75fafcec-f490-7dfc-9613-6774e04101b3"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("772b6c4c-533f-aad0-ead5-2dfe04cd1b99"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("78136f4f-95b0-3dcb-aab7-1e5fddd56d58"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("7b802917-d70f-3fb3-b5fb-5c08be70a7f2"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("7bbc132b-706c-a70c-f095-b54b444882a6"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("7ca6b2fd-1c0f-2d02-8858-71b7b02e35fe"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("7da53a76-2fb9-2a94-60b3-fae4111ab994"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("7ed5caa2-08d4-6cbe-f6e4-a94bc979a6a6"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("7f209fc9-b639-68b9-fce0-c4d0951f0246"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("7fa19dba-9062-b0e4-5029-2481a6d046d1"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("805e2f7f-da02-fe77-d387-31cd970fbe73"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("811ecad5-2cb9-2945-77bb-f9c1836155b9"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("83cf2f42-558a-c583-2b0b-b67610d3a16c"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("83f35470-5345-e566-c318-3ca9b226cac7"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("8889245e-a72b-768e-451a-635a82ed9a2a"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("8995b7a3-8595-8198-cbf8-5f24361b22ce"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("89b93d8e-8c5d-cf54-e206-186bae457e12"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("8d2d2b69-4ba0-3a8a-fba6-bd340241a1a2"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("8f91c15d-d901-3a7e-5568-3a2cffee2878"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("8ff271d7-01b2-1c3b-d4c7-116d0fc977cb"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("95ea4d09-5912-5547-7c56-f6052a5c9a36"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("9646e9ed-d6fa-c2cd-e6f3-abedfc1615c0"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("981b2ea8-d5cc-46bf-3c00-df8c1d5d3d35"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("9a0be359-195a-9891-e0fc-79cbe655d08d"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("9bfd928e-6859-c19b-e92d-6ed79f46bdbb"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("9e1f7442-7886-7772-f3ab-4ecae124a9e0"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("a0638854-c6d5-3e40-aa43-741a6b3c1665"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("a1e3df9b-60f2-13bc-d397-9d40f031336a"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("a8193c87-aa77-c110-90d2-493fc189f8bd"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("aadcf2e5-1db7-2d4d-f8c8-28795a53599e"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("abbf94be-17bc-f260-2e1f-f84252f4d6d7"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("b1fe7fd7-bae1-0eb4-4456-c13b88e06a79"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("b4b1b365-4b0b-1c60-4b86-fd1e3506e499"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("b932aecd-8f73-d5f8-bd82-87114a387d92"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("b9af1a94-ce5a-8914-f790-7f17dee89ca7"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("bf72d914-e561-1e18-68b3-53f4692a0ae7"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("c3cd493b-7894-feac-58d7-42ee4423950e"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("c40df9e2-3558-52df-375f-8665be57b289"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("c5d855d5-b70f-df14-178e-f1f4fcd4505e"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("c733bd7d-64d2-7944-ca57-261a00b6c930"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("cc327d47-a2d7-8de0-7577-299cb4bcb1b3"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("ce786369-a74f-269e-5d13-89d7ef9af150"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("d640bcdd-dbe0-8305-7733-a6aa07fea4d1"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("d7e754bf-f9b8-8318-eefc-ec1bd9e69c14"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("d7f010b1-4829-43f1-b764-8b5b29fda23c"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("d9dba26a-acb3-e438-6f7b-12fcf9650027"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("dfd0054d-48a7-608d-91b3-73ae482a8a91"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("dffef6e9-0aa7-7a49-67d4-42d2a0bafcfc"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("e0e3a15a-a7fe-340b-7164-a4c311ce1446"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("e14e9e44-f8fb-ab37-f83e-bd5979d40cf5"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("e1c8a801-962a-c1a0-e0ce-fa846e86e6fd"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("e259294e-6d8d-168b-9597-85079a216f39"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("e76012a2-89b2-964b-ed90-a739ec483048"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("e8f9096d-304a-2bb8-4eb3-83b3c524ac25"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("ed94e604-4294-1a61-2373-cbf82a138636"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("ee41765c-e68a-7f70-7a70-f6a9716c2298"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("ee576c08-9021-365d-64f4-fa77f4d27332"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("eec659a7-c008-9b34-78cb-beece23d7b60"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("ef67c555-c15f-79fa-0de4-9415b1424bd6"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("efd8ee29-7ff1-526c-aa00-c398c82e8603"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("f018bd1b-4ddb-2329-f8ab-b75060d2b7ec"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("f05817f2-4c5a-6821-1f51-e63acfb01f00"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("f0b4895a-f21b-da47-afa5-b470d4dbe44d"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("f92132ad-2f67-d7ad-9097-6679a51b40c6"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("f9ee1a56-b74a-2c4d-9fff-abb91688f592"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("fb53866d-deb6-bc2d-1a19-4b4e427ac174"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("fdf359bc-7272-e8b4-d69a-7c06fca4924c"));

            migrationBuilder.DeleteData(
                table: "CiselnikPolozky",
                keyColumn: "Id",
                keyValue: new Guid("ff34a2bb-7df2-6610-759d-7e3b9a800718"));

            migrationBuilder.DeleteData(
                table: "Ciselniky",
                keyColumn: "Id",
                keyValue: new Guid("147cf25b-76ae-16a7-170e-63783ef2c54e"));

            migrationBuilder.DeleteData(
                table: "Ciselniky",
                keyColumn: "Id",
                keyValue: new Guid("447f5c6c-7a98-350e-4de5-2147662ef433"));

            migrationBuilder.DeleteData(
                table: "Ciselniky",
                keyColumn: "Id",
                keyValue: new Guid("4c78e141-6928-8ef6-2e00-d46969dd606e"));

            migrationBuilder.DeleteData(
                table: "Ciselniky",
                keyColumn: "Id",
                keyValue: new Guid("55915f34-7205-32e7-7058-bc74a0903b84"));

            migrationBuilder.DeleteData(
                table: "Ciselniky",
                keyColumn: "Id",
                keyValue: new Guid("5907781a-9d4e-3f79-94fa-6ab75eccf6bf"));

            migrationBuilder.DeleteData(
                table: "Ciselniky",
                keyColumn: "Id",
                keyValue: new Guid("7942c0f1-6a00-a2e7-30bb-d3b33fa2727b"));

            migrationBuilder.DeleteData(
                table: "Ciselniky",
                keyColumn: "Id",
                keyValue: new Guid("9052b182-f12b-60ff-1f40-9bae43883e7b"));

            migrationBuilder.DeleteData(
                table: "Ciselniky",
                keyColumn: "Id",
                keyValue: new Guid("b21af6d9-b28a-98c8-ee88-818593a2fbe2"));

            migrationBuilder.DeleteData(
                table: "Ciselniky",
                keyColumn: "Id",
                keyValue: new Guid("c8be1992-0320-35db-5dbd-0262acebb01c"));
        }
    }
}
