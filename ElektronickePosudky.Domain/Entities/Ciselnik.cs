namespace ElektronickePosudky.Domain.Entities;

public class Ciselnik
{
    public Guid Id { get; private set; }
    public string Kod { get; private set; } = string.Empty;
    public string Verze { get; private set; } = string.Empty;
    public DateTime PlatnostOd { get; private set; }
    public DateTime? PlatnostDo { get; private set; }

    public string? PrekladyJson { get; private set; }

    protected Ciselnik() { }

    public Ciselnik(Guid id, string kod, string verze, DateTime platnostOd, DateTime? platnostDo, string? prekladyJson)
    {
        Id = id;
        Kod = kod;
        Verze = verze;
        PlatnostOd = platnostOd;
        PlatnostDo = platnostDo;
        PrekladyJson = prekladyJson;
    }
}