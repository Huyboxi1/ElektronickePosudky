using ElektronickePosudky.Domain.ValueObjects;

namespace ElektronickePosudky.Domain.Entities.PosudekAggregate;

public class PosudekHarmonizovanyKod
{
    public Guid Id { get; private set; }
    public Guid PosudekZpusobilostId { get; private set; }

    public CiselnikPolozkaReference HarmonizovanyKod { get; private set; } = null!;
    public CiselnikPolozkaReference? UpresneniKod { get; private set; }
    public string? UpresneniText { get; private set; }

    private readonly List<CiselnikPolozkaReference> _skupinaRo = [];
    public IReadOnlyCollection<CiselnikPolozkaReference> SkupinaRo => _skupinaRo.AsReadOnly();

    protected PosudekHarmonizovanyKod() { }

    public PosudekHarmonizovanyKod(
        CiselnikPolozkaReference kod,
        CiselnikPolozkaReference? upresneniKod = null,
        string? upresneniText = null)
    {
        Id = Guid.NewGuid();
        HarmonizovanyKod = kod;
        UpresneniKod = upresneniKod;
        UpresneniText = upresneniText;
    }

    public void AddSkupinaRo(CiselnikPolozkaReference skupinaRo) => _skupinaRo.Add(skupinaRo);
}