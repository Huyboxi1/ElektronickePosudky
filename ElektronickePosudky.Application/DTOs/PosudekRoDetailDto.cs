namespace ElektronickePosudky.Application.DTOs;

public class TranslationItemDto
{
    public string Nazev { get; set; } = string.Empty;
    public string Popis { get; set; } = string.Empty;
}

public class CiselnikPolozkaReferenceDto
{

    public string CiselnikKod { get; set; } = string.Empty;
    public string CiselnikVerze { get; set; } = string.Empty;
    public string PolozkaKod { get; set; } = string.Empty;
    public Dictionary<string, TranslationItemDto> Preklady { get; set; } = new();
}

public class PacientDetailDto
{
    public string Rid { get; set; } = string.Empty;
    public string Jmeno { get; set; } = string.Empty;
    public string Prijmeni { get; set; } = string.Empty;
    public DateTime DatumNarozeni { get; set; }
    public string Adresa { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Doklad { get; set; } = string.Empty;
    public string Pohlavi { get; set; } = string.Empty;
}

public class ZdravotnickyPracovnikDetailDto
{
    public string KrzpId { get; set; } = string.Empty;
    public string TitulPred { get; set; } = string.Empty;
    public string Jmeno { get; set; } = string.Empty;
    public string Prijmeni { get; set; } = string.Empty;
    public string TitulZa { get; set; } = string.Empty;
    public string Odbornost { get; set; } = string.Empty;
}

public class PoskytovatelDetailDto
{
    public string Ico { get; set; } = string.Empty;
    public string Nazev { get; set; } = string.Empty;
    public string Adresa { get; set; } = string.Empty;
}

public class SkupinaRidicskehoOpravneniDetailDto
{
    public CiselnikPolozkaReferenceDto SkupinaRo { get; set; } = null!;
}

public class HarmonizovanyKodDetailResponseDto
{
    public CiselnikPolozkaReferenceDto HarmonizovanyKod { get; set; } = null!;
    public List<CiselnikPolozkaReferenceDto> SkupinaRo { get; set; } = new();
    public CiselnikPolozkaReferenceDto? UpresneniKod { get; set; }
    public string? UpresneniText { get; set; }
}

public class NarodniKodDetailResponseDto
{
    public CiselnikPolozkaReferenceDto NarodniKod { get; set; } = null!;
    public CiselnikPolozkaReferenceDto SkupinaRo { get; set; } = null!;
    public string? UpresneniText { get; set; }
}

public class PosudekRoDetailDto
{
    public Guid Id { get; set; }

    public PacientDetailDto Pacient { get; set; } = null!;
    public ZdravotnickyPracovnikDetailDto ZdravotnickyPracovnik { get; set; } = null!;
    public PoskytovatelDetailDto PoskytovatelZdravotnickychSluzeb { get; set; } = null!;

    public CiselnikPolozkaReferenceDto OdbornostLekare { get; set; } = null!;
    public CiselnikPolozkaReferenceDto StavPosudku { get; set; } = null!;
    public CiselnikPolozkaReferenceDto DruhProhlidky { get; set; } = null!;
    public CiselnikPolozkaReferenceDto DruhPosudku { get; set; } = null!;

    public CiselnikPolozkaReferenceDto Vysledek { get; set; } = null!;
    public CiselnikPolozkaReferenceDto SkupinaZadatelRidic { get; set; } = null!;

    public DateTime DatumVystaveni { get; set; }
    public DateTime? PlatnostDo { get; set; }
    public DateTime DatumVytvoreni { get; set; }

    public List<SkupinaRidicskehoOpravneniDetailDto> SkupinyRidicskehoOpravneni { get; set; } = new();
    public List<HarmonizovanyKodDetailResponseDto> HarmonizovaneKody { get; set; } = new();
    public List<NarodniKodDetailResponseDto> NarodniKody { get; set; } = new();

    public string VerzeZaznamu { get; set; } = string.Empty;
}