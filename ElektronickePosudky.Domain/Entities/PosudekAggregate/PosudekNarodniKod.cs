using ElektronickePosudky.Domain.ValueObjects;

namespace ElektronickePosudky.Domain.Entities.PosudekAggregate;

public class PosudekNarodniKod
{
    public Guid Id { get; private set; }
    public Guid PosudekZpusobilostId { get; private set; }

    public CiselnikPolozkaReference NarodniKod { get; private set; } = null!;
    public CiselnikPolozkaReference SkupinaRo { get; private set; } = null!;
    public string? UpresneniText { get; private set; }

    protected PosudekNarodniKod() { }

    public PosudekNarodniKod(
        CiselnikPolozkaReference narodniKod,
        CiselnikPolozkaReference skupinaRo,
        string? upresneniText = null)
    {
        Id = Guid.NewGuid();
        NarodniKod = narodniKod;
        SkupinaRo = skupinaRo;
        UpresneniText = upresneniText;
    }
}
