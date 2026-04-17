namespace ElektronickePosudky.Domain.Entities;

public class CiselnikPolozka
{
    public Guid Id { get; private set; }
    public string CiselnikKod { get; private set; } = string.Empty;
    public string Kod { get; private set; } = string.Empty;
    public string Verze { get; private set; } = string.Empty;
    public Guid? RodicId { get; private set; }
    public string? PrekladyJson { get; private set; }

    protected CiselnikPolozka() { }

    public CiselnikPolozka(Guid id, string ciselnikKod, string kod, string verze, Guid? rodicId, string? prekladyJson)
    {
        Id = id;
        CiselnikKod = ciselnikKod;
        Kod = kod;
        Verze = verze;
        RodicId = rodicId;
        PrekladyJson = prekladyJson;
    }
}