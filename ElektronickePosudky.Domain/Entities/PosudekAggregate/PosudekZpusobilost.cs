using ElektronickePosudky.Domain.ValueObjects;

namespace ElektronickePosudky.Domain.Entities.PosudekAggregate;

public class PosudekZpusobilost
{
    public Guid Id { get; private set; }
    public Guid PosudekRoId { get; private set; }

    public CiselnikPolozkaReference SkupinaZadateleRidic { get; private set; } = null!;
    public CiselnikPolozkaReference Vysledek { get; private set; } = null!;
    public string VerzeZaznamu { get; private set; } = string.Empty;

    private readonly List<PosudekSkupinaRo> _skupinyRidicskehoOpravneni = new();
    public IReadOnlyCollection<PosudekSkupinaRo> SkupinyRidicskehoOpravneni => _skupinyRidicskehoOpravneni.AsReadOnly();

    private readonly List<PosudekHarmonizovanyKod> _harmonizovaneKody = [];
    public IReadOnlyCollection<PosudekHarmonizovanyKod> HarmonizovaneKody => _harmonizovaneKody.AsReadOnly();

    private readonly List<PosudekNarodniKod> _narodniKody = [];
    public IReadOnlyCollection<PosudekNarodniKod> NarodniKody => _narodniKody.AsReadOnly();

    protected PosudekZpusobilost() { }

    public PosudekZpusobilost(CiselnikPolozkaReference skupinaZadatele, CiselnikPolozkaReference vysledek)
    {
        Id = Guid.NewGuid();
        SkupinaZadateleRidic = skupinaZadatele;
        Vysledek = vysledek;
        VerzeZaznamu = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }

    public void SetPosudekId(Guid posudekRoId)
    {
        PosudekRoId = posudekRoId;
    }

    public void AddSkupinaRidicskehoOpravneni(PosudekSkupinaRo skupina) => _skupinyRidicskehoOpravneni.Add(skupina);
    public void AddHarmonizovanyKod(PosudekHarmonizovanyKod kod) => _harmonizovaneKody.Add(kod);
    public void AddNarodniKod(PosudekNarodniKod kod) => _narodniKody.Add(kod);
}