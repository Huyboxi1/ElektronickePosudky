using ElektronickePosudky.Domain.ValueObjects;

namespace ElektronickePosudky.Domain.Entities.PosudekAggregate;

public class PosudekHistorie
{
    public Guid Id { get; private set; }
    public Guid PosudekRoId { get; private set; }

    public CiselnikPolozkaReference TypOperace { get; private set; } = null!;
    public DateTime DatumOperace { get; private set; }
    public ZdravotnickyPracovnikVO Lekar { get; private set; } = null!;
    public PoskytovatelVO Poskytovatel { get; private set; } = null!;

    protected PosudekHistorie() { }

    public PosudekHistorie(
        CiselnikPolozkaReference typOperace,
        DateTime datumOperace,
        ZdravotnickyPracovnikVO lekar,
        PoskytovatelVO poskytovatel)
    {
        Id = Guid.NewGuid();
        TypOperace = typOperace;
        DatumOperace = datumOperace;
        Lekar = lekar;
        Poskytovatel = poskytovatel;
    }

    public void SetPosudekId(Guid posudekRoId)
    {
        PosudekRoId = posudekRoId;
    }
}