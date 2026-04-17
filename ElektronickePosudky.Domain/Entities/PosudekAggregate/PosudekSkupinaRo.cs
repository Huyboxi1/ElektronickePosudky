using ElektronickePosudky.Domain.ValueObjects;

namespace ElektronickePosudky.Domain.Entities.PosudekAggregate;

public class PosudekSkupinaRo
{
    public Guid Id { get; private set; }
    public Guid PosudekZpusobilostId { get; private set; }
    public CiselnikPolozkaReference SkupinaRo { get; private set; } = null!;

    protected PosudekSkupinaRo() { }

    public PosudekSkupinaRo(CiselnikPolozkaReference skupinaRo)
    {
        Id = Guid.NewGuid();
        SkupinaRo = skupinaRo;
    }
}