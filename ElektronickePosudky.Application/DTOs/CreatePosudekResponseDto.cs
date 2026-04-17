namespace ElektronickePosudky.Application.DTOs;

public class PosudekHlavickaResponseDto
{
    public PacientDetailDto Pacient { get; set; } = null!;
    public ZdravotnickyPracovnikDetailDto ZdravotnickyPracovnik { get; set; } = null!;
    public PoskytovatelDetailDto PoskytovatelZdravotnickychSluzeb { get; set; } = null!;

    public CiselnikPolozkaReferenceDto OdbornostLekare { get; set; } = null!;
    public CiselnikPolozkaReferenceDto StavPosudku { get; set; } = null!;
    public CiselnikPolozkaReferenceDto DruhProhlidky { get; set; } = null!;
    public CiselnikPolozkaReferenceDto DruhPosudku { get; set; } = null!;

    public DateTime DatumVystaveni { get; set; }
    public DateTime? PlatnostDo { get; set; }
    public DateTime DatumVytvoreni { get; set; }
}

public class PosudekZpusobilostResponseDto
{
    public Guid PosudekId { get; set; }

    public CiselnikPolozkaReferenceDto SkupinaZadateleRidic { get; set; } = null!;
    public List<SkupinaRidicskehoOpravneniDetailDto> SkupinyRidicskehoOpravneni { get; set; } = new();
    public CiselnikPolozkaReferenceDto Vysledek { get; set; } = null!;
    public List<HarmonizovanyKodDetailResponseDto> HarmonizovaneKody { get; set; } = new();
    public List<NarodniKodDetailResponseDto> NarodniKody { get; set; } = new();

    public string VerzeZaznamu { get; set; } = string.Empty;
}

public class CreatePosudekResponseDto
{
    public Guid Id { get; set; }
    public PosudekHlavickaResponseDto Hlavicka { get; set; } = null!;
    public List<PosudekZpusobilostResponseDto> Zpusobilosti { get; set; } = new();
}
