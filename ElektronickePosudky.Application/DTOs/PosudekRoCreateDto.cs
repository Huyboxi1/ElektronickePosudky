namespace ElektronickePosudky.Application.DTOs;

public class CodebookItemDto
{
    public string Kod { get; set; } = string.Empty;
    public string Verze { get; set; } = string.Empty;
}

public class HarmonizovanyKodDetailDto
{
    public CodebookItemDto HarmonizovanyKod { get; set; } = null!;
    public List<CodebookItemDto> SkupinaRo { get; set; } = new();
    public string? UpresneniText { get; set; }
}

public class NarodniKodDetailDto
{
    public CodebookItemDto NarodniKod { get; set; } = null!;
    public CodebookItemDto SkupinaRo { get; set; } = null!;
    public string? UpresneniText { get; set; }
}

public class PosudekSkupinaRoDetailDto
{
    public CodebookItemDto SkupinaRo { get; set; } = null!;
}

public class PosudekZpusobilostDto
{
    public CodebookItemDto SkupinaZadateleRidic { get; set; } = null!;
    public List<PosudekSkupinaRoDetailDto> SkupinyRidicskehoOpravneni { get; set; } = new();
    public CodebookItemDto Vysledek { get; set; } = null!;
    public List<HarmonizovanyKodDetailDto> HarmonizovaneKody { get; set; } = new();
    public List<NarodniKodDetailDto> NarodniKody { get; set; } = new();
}

public class PosudekRoCreateDto
{
    public string Rid { get; set; } = string.Empty;
    public string KrzpId { get; set; } = string.Empty;
    public CodebookItemDto TypAkce { get; set; } = null!;
    public CodebookItemDto StavPosudku { get; set; } = null!;
    public CodebookItemDto DruhProhlidky { get; set; } = null!;
    public CodebookItemDto DruhPosudku { get; set; } = null!;
    public DateTime DatumVystaveni { get; set; }
    public DateTime? PlatnostDo { get; set; }
    public Guid? OpakovanyPosudekId { get; set; }
    public List<PosudekZpusobilostDto> Zpusobilosti { get; set; } = new();
}