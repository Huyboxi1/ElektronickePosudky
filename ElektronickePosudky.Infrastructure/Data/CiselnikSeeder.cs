using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using ElektronickePosudky.Domain.Entities;

namespace ElektronickePosudky.Infrastructure.Data;

public static class CiselnikSeeder
{
    private static readonly DateTime PlatnostOd = new(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private const string Verze = "1.0.0";

    private static Guid ToGuid(string input)
    {
        using var md5 = MD5.Create();
        var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
        return new Guid(hash);
    }

    public static IEnumerable<Ciselnik> GetCiselniky()
    {
        var ciselniky = new List<Ciselnik>();

        void AddCiselnik(string kod, string nameEn)
        {
            var preklady = JsonSerializer.Serialize(new { Description_en = nameEn });
            ciselniky.Add(new Ciselnik(ToGuid(kod), kod, Verze, PlatnostOd, null, preklady));
        }

        AddCiselnik("akce-ro", "Action type");
        AddCiselnik("druh-prohlidky-ro", "Type of medical examination performed");
        AddCiselnik("skupina-zadatel-ridic-ro", "Group of applicants, holders of driving licences");
        AddCiselnik("vysledek-posudku-ro", "Result of the report for the MA");
        AddCiselnik("druh-posudku-ro", "Type of report for the MA");
        AddCiselnik("seznam-skupin-ro", "Driving License Group");
        AddCiselnik("seznam-harmonizovane-kody-ro", "Harmonised codes for the restriction of Mas");
        AddCiselnik("seznam-narodni-kody-ro", "National codes for MA restrictions");
        AddCiselnik("stav-posudku", "Medical Opinion Status");

        return ciselniky;
    }

    public static IEnumerable<CiselnikPolozka> GetCiselnikPolozky()
    {
        var polozky = new List<CiselnikPolozka>();

        void AddItems(string ciselnikKod, params (string kod, string nameCs, string descCs)[] items)
        {
            foreach (var item in items)
            {
                var prekladyJson = JsonSerializer.Serialize(new
                {
                    Name_cs = item.nameCs,
                    Description_cs = item.descCs
                });

                polozky.Add(new CiselnikPolozka(
                    id: ToGuid($"{ciselnikKod}_{item.kod}"),
                    ciselnikKod: ciselnikKod,
                    kod: item.kod,
                    verze: Verze,
                    rodicId: null,
                    prekladyJson: prekladyJson
                ));
            }
        }

        AddItems("akce-ro",
            ("akce_ro_1", "vytvoření", "Vytvoření lékařského posudku."),
            ("akce_ro_2", "aktualizace", "Aktualizace lékařského posudku."),
            ("akce_ro_3", "zneplatnění", "Zneplatnění lékařského posudku.")
        );

        AddItems("druh-prohlidky-ro",
            ("druh_prohlidky_ro_1", "vstupní", "Vstupní lékařská prohlídka."),
            ("druh_prohlidky_ro_2", "pravidelná", "Pravidelná lékařská prohlídka."),
            ("druh_prohlidky_ro_3", "mimořádná", "Mimořádná lékařská prohlídka.")
        );

        AddItems("skupina-zadatel-ridic-ro",
            ("skupina_ro_1", "skupina 1", "Žadatelé a držitelé řidičských oprávnění definovaných vyhláškou 277/2004 Sb."),
            ("skupina_ro_2", "skupina 2", "Žadatelé a držitelé řidičských oprávnění definovaných vyhláškou 277/2004 Sb.")
        );

        AddItems("vysledek-posudku-ro",
            ("vysledek_posudku_ro_1", "způsobilý", "Zdravotní způsobilost k řízení motorových vozidel."),
            ("vysledek_posudku_ro_2", "nezpůsobilý", "Zdravotní nezpůsobilost k řízení motorových vozidel."),
            ("vysledek_posudku_ro_3", "způsobilý s podmínkou", "Zdravotní způsobilost k řízení motorových vozidel s podmínkou.")
        );

        AddItems("druh-posudku-ro",
            ("druh_posudku_ro_1", "prvořidič", "Vydání řidičského oprávnění (prvořidič)."),
            ("druh_posudku_ro_2", "rozšíření řidičského oprávnění", "Rozšíření řidičského oprávnění."),
            ("druh_posudku_ro_3", "prodloužení řidičského oprávnění", "Prodloužení řidičského oprávnění."),
            ("druh_posudku_ro_4", "senioři", "Řidičské oprávnění pro seniory."),
            ("druh_posudku_ro_5", "přezkoumání způsobilosti", "Přezkoumání způsobilosti.")
        );

        AddItems("seznam-skupin-ro",
            ("AM", "skupina AM", "Skupina AM"),
            ("A1", "skupina A1", "Skupina A1"),
            ("A2", "skupina A2", "Skupina A2"),
            ("A", "skupina A", "Skupina A"),
            ("B1", "skupina B1", "Skupina B1"),
            ("B", "skupina B", "Skupina B"),
            ("BE", "skupina BE", "Skupina BE"),
            ("C1", "skupina C1", "Skupina C1"),
            ("C1E", "skupina C1E", "Skupina C1E"),
            ("C", "skupina C", "Skupina C"),
            ("CE", "skupina CE", "Skupina CE"),
            ("D1", "skupina D1", "Skupina D1"),
            ("D1E", "skupina D1E", "Skupina D1E"),
            ("D", "skupina D", "Skupina D"),
            ("DE", "skupina DE", "Skupina DE"),
            ("T", "skupina T", "Skupina T")
        );

        AddItems("seznam-harmonizovane-kody-ro",
            ("01.01", "01.01 Brýle", "01.01 Brýle"),
            ("01.02", "01.02 Kontaktní čočky", "01.02 Kontaktní čočky"),
            ("01.05", "01.05 Oční kryt", "01.05 Oční kryt"),
            ("01.06", "01.06 Brýle nebo kontaktní čočky", "01.06 Brýle nebo kontaktní čočky"),
            ("01.07", "01.07 Specifické optické pomůcky", "01.07 Specifické optické pomůcky"),
            ("02", "02. Sluchové pomůcky/komunikační pomůcky", "02. Sluchové pomůcky/komunikační pomůcky"),
            ("03.01", "03.01 Protéza/ortéza horní končetiny", "03.01 Protéza/ortéza horní končetiny"),
            ("03.02", "03.02 Protéza/ortéza dolní končetiny", "03.02 Protéza/ortéza dolní končetiny"),
            ("10.02", "10.02 Automatická převodovka", "10.02 Automatická převodovka"),
            ("10.04", "10.04 Přizpůsobené ústrojí ovládání převodovky", "10.04 Přizpůsobené ústrojí ovládání převodovky"),
            ("15.01", "15.01 Přizpůsobený pedál spojky", "15.01 Přizpůsobený pedál spojky"),
            ("15.02", "15.02 Ručně ovládaná spojka", "15.02 Ručně ovládaná spojka"),
            ("15.03", "15.03 Automatická spojka", "15.03 Automatická spojka"),
            ("15.04", "15.04 Opatření proti zablokování nebo aktivaci pedálu spojky", "15.04 Opatření proti zablokování nebo aktivaci pedálu spojky"),
            ("20.01", "20.01 Přizpůsobený brzdový pedál", "20.01 Přizpůsobený brzdový pedál"),
            ("20.03", "20.03 Brzdový pedál upravený na levou nohu", "20.03 Brzdový pedál upravený na levou nohu"),
            ("20.04", "20.04 Posuvný brzdový pedál", "20.04 Posuvný brzdový pedál"),
            ("20.05", "20.05 Sklopený brzdový pedál", "20.05 Sklopený brzdový pedál"),
            ("20.06", "20.06 Ručně ovládaná provozní brzda", "20.06 Ručně ovládaná provozní brzda"),
            ("20.07", "20.07 Ovládání brzdy s použitím maximální síly ... N", "20.07 Ovládání brzdy s použitím maximální síly ... N"),
            ("20.09", "20.09 Přizpůsobená parkovací brzda", "20.09 Přizpůsobená parkovací brzda"),
            ("20.12", "20.12 Opatření proti zablokování nebo aktivaci brzdového pedálu", "20.12 Opatření proti zablokování nebo aktivaci brzdového pedálu"),
            ("20.13", "20.13 Ovládání brzdy kolenem", "20.13 Ovládání brzdy kolenem"),
            ("20.14", "20.14 Ovládání brzdového systému s podporou vnější síly", "20.14 Ovládání brzdového systému s podporou vnější síly"),
            ("25.01", "25.01 Přizpůsobený pedál akcelerátoru", "25.01 Přizpůsobený pedál akcelerátoru"),
            ("25.03", "25.03 Sklopený pedál akcelerátoru", "25.03 Sklopený pedál akcelerátoru"),
            ("25.04", "25.04 Ručně ovládaný akcelerátor", "25.04 Ručně ovládaný akcelerátor"),
            ("25.05", "25.05 Ovládání akcelerátoru kolenem", "25.05 Ovládání akcelerátoru kolenem"),
            ("25.06", "25.06 Ovládání akcelerátoru s podporou vnější síly", "25.06 Ovládání akcelerátoru s podporou vnější síly"),
            ("25.08", "25.08 Pedál akcelerátoru nalevo", "25.08 Pedál akcelerátoru nalevo"),
            ("25.09", "25.09 Opatření proti zablokování nebo aktivaci akcelerátoru", "25.09 Opatření proti zablokování nebo aktivaci akcelerátoru"),
            ("31.01", "31.01 Další sada paralelních pedálů", "31.01 Další sada paralelních pedálů"),
            ("31.02", "31.02 Pedály ve stejné (nebo téměř stejné) úrovni", "31.02 Pedály ve stejné (nebo téměř stejné) úrovni"),
            ("31.03", "31.03 Opatření proti zablokování akcelerátoru a brzd", "31.03 Opatření proti zablokování akcelerátoru a brzd"),
            ("31.04", "31.04 Zvýšená podlaha", "31.04 Zvýšená podlaha"),
            ("32.01", "32.01 Akcelerátor a provozní brzda jako systém ovládaný jednou rukou", "32.01 Akcelerátor a provozní brzda jako systém ovládaný jednou rukou"),
            ("32.02", "32.02 Akcelerátor a provozní brzda jako systém ovládaný vnější silou", "32.02 Akcelerátor a provozní brzda jako systém ovládaný vnější silou"),
            ("33.01", "33.01 Akcelerátor, brzda a řízení ovládané jednou rukou", "33.01 Akcelerátor, brzda a řízení ovládané jednou rukou"),
            ("33.02", "33.02 Akcelerátor, brzda a řízení ovládané oběma rukama", "33.02 Akcelerátor, brzda a řízení ovládané oběma rukama"),
            ("35.02", "35.02 Ovladače ovladatelné bez puštění řízení", "35.02 Ovladače ovladatelné bez puštění řízení"),
            ("35.03", "35.03 Ovladače ovladatelné levou rukou bez puštění řízení", "35.03 Ovladače ovladatelné levou rukou bez puštění řízení"),
            ("35.04", "35.04 Ovladače ovladatelné pravou rukou bez puštění řízení", "35.04 Ovladače ovladatelné pravou rukou bez puštění řízení"),
            ("35.05", "35.05 Ovladače ovladatelné bez puštění řízení a mechanismy pedálů", "35.05 Ovladače ovladatelné bez puštění řízení a mechanismy pedálů"),
            ("40.01", "40.01 Řízení s použitím maximální ovládací síly", "40.01 Řízení s použitím maximální ovládací síly"),
            ("40.05", "40.05 Přizpůsobený volant", "40.05 Přizpůsobený volant"),
            ("40.06", "40.06 Přizpůsobená poloha volantu", "40.06 Přizpůsobená poloha volantu"),
            ("40.09", "40.09 Nožní ovládání řízení", "40.09 Nožní ovládání řízení"),
            ("40.11", "40.11 Pomocná zařízení na volantu", "40.11 Pomocná zařízení na volantu"),
            ("40.14", "40.14 Systém řízení ovládaný jednou rukou/paží", "40.14 Systém řízení ovládaný jednou rukou/paží"),
            ("40.15", "40.15 Systém řízení ovládaný dvěma rukama/pažemi", "40.15 Systém řízení ovládaný dvěma rukama/pažemi"),
            ("42.01", "42.01 Přizpůsobené zařízení pro výhled dozadu", "42.01 Přizpůsobené zařízení pro výhled dozadu"),
            ("42.03", "42.03 Přídavné vnitřní zařízení umožňující výhled do stran", "42.03 Přídavné vnitřní zařízení umožňující výhled do stran"),
            ("42.05", "42.05 Zařízení k eliminaci mrtvého úhlu", "42.05 Zařízení k eliminaci mrtvého úhlu"),
            ("43.01", "43.01 Výška sedadla řidiče umožňující normální výhled", "43.01 Výška sedadla řidiče umožňující normální výhled"),
            ("43.02", "43.02 Anatomicky přizpůsobené sedadlo řidiče", "43.02 Anatomicky přizpůsobené sedadlo řidiče"),
            ("43.03", "43.03 Sedadlo řidiče s postranní opěrou", "43.03 Sedadlo řidiče s postranní opěrou"),
            ("43.04", "43.04 Sedadlo řidiče s opěrkou ruky", "43.04 Sedadlo řidiče s opěrkou ruky"),
            ("43.06", "43.06 Přizpůsobení bezpečnostního pásu", "43.06 Přizpůsobení bezpečnostního pásu"),
            ("43.07", "43.07 Typ bezpečnostního pásu s podporou pro dobrou stabilitu", "43.07 Typ bezpečnostního pásu s podporou pro dobrou stabilitu"),
            ("44.01", "44.01 Samostatně ovládaná brzda", "44.01 Samostatně ovládaná brzda"),
            ("44.02", "44.02 Přizpůsobená brzda na předním kole", "44.02 Přizpůsobená brzda na předním kole"),
            ("44.03", "44.03 Přizpůsobená brzda na zadním kole", "44.03 Přizpůsobená brzda na zadním kole"),
            ("44.04", "44.04 Přizpůsobená rukojeť akcelerátoru", "44.04 Přizpůsobená rukojeť akcelerátoru"),
            ("44.08", "44.08 Výška sedadla umožňující obě nohy na zemi", "44.08 Výška sedadla umožňující obě nohy na zemi"),
            ("44.09", "44.09 Maximální ovládací síla brzdy předního kola", "44.09 Maximální ovládací síla brzdy předního kola"),
            ("44.10", "44.10 Maximální ovládací síla brzdy zadního kola", "44.10 Maximální ovládací síla brzdy zadního kola"),
            ("44.11", "44.11 Přizpůsobená stupačka", "44.11 Přizpůsobená stupačka"),
            ("44.12", "44.12 Přizpůsobená řídítka", "44.12 Přizpůsobená řídítka"),
            ("45", "45. Motocykl pouze s postranním vozíkem", "45. Motocykl pouze s postranním vozíkem"),
            ("46", "46. Pouze pro tříkolová motorová vozidla", "46. Pouze pro tříkolová motorová vozidla"),
            ("47", "47. Pouze pro vozidla s více než dvěma koly", "47. Pouze pro vozidla s více než dvěma koly"),
            ("50", "50. Omezení na určité vozidlo/číslo podvozku (VIN)", "50. Omezení na určité vozidlo/číslo podvozku (VIN)"),
            ("61", "61. Omezení jízdy podle denní doby", "61. Omezení jízdy podle denní doby"),
            ("62", "62. Omezení jízdy v okruhu ... km", "62. Omezení jízdy v okruhu ... km"),
            ("63", "63. Řízení vozidla bez cestujících", "63. Řízení vozidla bez cestujících"),
            ("64", "64. Jízda rychlostí nepřesahující ... km/h", "64. Jízda rychlostí nepřesahující ... km/h"),
            ("65", "65. Řízení vozidla povoleno výhradně v doprovodu", "65. Řízení vozidla povoleno výhradně v doprovodu"),
            ("66", "66. Bez přívěsu", "66. Bez přívěsu"),
            ("67", "67. Zákaz jízdy na dálnici", "67. Zákaz jízdy na dálnici"),
            ("78", "78. Pouze pro vozidla s automatickou převodovkou", "78. Pouze pro vozidla s automatickou převodovkou")
        );

        AddItems("seznam-narodni-kody-ro",
            ("105", "105. Jiná zdravotní omezení", "105. Jiná zdravotní omezení, která nejsou uvedena v části I této přílohy"),
            ("111a", "111. Nelze vykonávat činnost: a) řidiče v pracovněprávním vztahu", "111. Nelze vykonávat činnost: a) řidiče v pracovněprávním vztahu"),
            ("111b", "111. Nelze vykonávat činnost: b) řidiče OSVČ", "111. Nelze vykonávat činnost: b) řidiče OSVČ"),
            ("111c", "111. Nelze vykonávat činnost: c) učitele výcviku v řízení", "111. Nelze vykonávat činnost: c) učitele výcviku v řízení"),
            ("115", "115. Posilovač spojky", "115. Posilovač spojky"),
            ("160", "160. Výjimka z věku u řidičského oprávnění pro sportovní soutěž", "160. Výjimka z věku u řidičského oprávnění pro sportovní soutěž"),
            ("172", "172. Omezení skupiny A pouze k řízení motorového vozíku pro invalidy", "172. Omezení skupiny A pouze k řízení motorového vozíku pro invalidy"),
            ("175", "175. Omezení skupiny D pouze k řízení vozidla MHD", "175. Omezení skupiny D pouze k řízení vozidla MHD"),
            ("185", "185. Pouze pro řízení motorových vozidel dle § 83 odst. 5", "185. Pouze pro řízení motorových vozidel dle § 83 odst. 5"),
            ("199", "199. Zkušební doba podmíněného upuštění od výkon", "199. Zkušební doba podmíněného upuštění od výkon")
        );

        AddItems("stav-posudku",
            ("stav_posudku_1", "platný", "platný"),
            ("stav_posudku_2", "neplatný", "neplatný"),
            ("stav_posudku_3", "zneplatněný", "zneplatněný")
        );

        return polozky;
    }
}