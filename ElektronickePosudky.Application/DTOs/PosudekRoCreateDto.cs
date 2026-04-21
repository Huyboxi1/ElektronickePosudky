namespace ElektronickePosudky.Application.DTOs;

public class HarmonizovanyKodCreateDto
{
    public string HarmonizovanyKod { get; set; } = string.Empty;

    public List<string> SkupinaRoKody { get; set; } = new();

    public string? UpresneniText { get; set; }
}

public class NarodniKodCreateDto
{
    public string NarodniKod { get; set; } = string.Empty;

    public string SkupinaRoKod { get; set; } = string.Empty;

    public string? UpresneniText { get; set; }
}

public class PosudekZpusobilostCreateDto
{
    public string SkupinaZadateleRidicKod { get; set; } = string.Empty;

    public List<string> SkupinyRidicskehoOpravneniKody { get; set; } = new();

    public string VysledekKod { get; set; } = string.Empty;

    public List<HarmonizovanyKodCreateDto> HarmonizovaneKody { get; set; } = new();
    public List<NarodniKodCreateDto> NarodniKody { get; set; } = new();
}

public class PosudekRoCreateDto
{
    public string Rid { get; set; } = string.Empty;
    public string KrzpId { get; set; } = string.Empty;

    public string TypAkceKod { get; set; } = string.Empty;
    public string StavPosudkuKod { get; set; } = string.Empty;
    public string DruhProhlidkyKod { get; set; } = string.Empty;
    public string DruhPosudkuKod { get; set; } = string.Empty;

    public DateTime DatumVystaveni { get; set; }
    public DateTime? PlatnostDo { get; set; }
    public Guid? OpakovanyPosudekId { get; set; }

    public List<PosudekZpusobilostCreateDto> Zpusobilosti { get; set; } = new();
}