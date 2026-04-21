using ElektronickePosudky.Domain.ValueObjects;

namespace ElektronickePosudky.Domain.Entities.PosudekAggregate;

public class PosudekHlavicka
{
    public PacientVO Pacient { get; private set; } = null!;
    public ZdravotnickyPracovnikVO ZdravotnickyPracovnik { get; private set; } = null!;
    public PoskytovatelVO PoskytovatelZdravotnickychSluzeb { get; private set; } = null!;

    public CiselnikPolozkaReference OdbornostLekare { get; private set; } = null!;
    public CiselnikPolozkaReference TypAkce { get; private set; } = null!;
    public CiselnikPolozkaReference StavPosudku { get; private set; } = null!;
    public CiselnikPolozkaReference DruhProhlidky { get; private set; } = null!;
    public CiselnikPolozkaReference DruhPosudku { get; private set; } = null!;

    public DateTime DatumVystaveni { get; private set; }
    public DateTime? PlatnostDo { get; private set; }
    public DateTime DatumVytvoreni { get; private set; }
    public string VerzeZaznamu { get; private set; } = string.Empty;

    private PosudekHlavicka()
    {
    }

    public PosudekHlavicka(
        PacientVO pacient,
        ZdravotnickyPracovnikVO zdravotnickyPracovnik,
        PoskytovatelVO poskytovatelZdravotnickychSluzeb,
        CiselnikPolozkaReference odbornostLekare,
        CiselnikPolozkaReference typAkce,
        CiselnikPolozkaReference stavPosudku,
        CiselnikPolozkaReference druhProhlidky,
        CiselnikPolozkaReference druhPosudku,
        DateTime datumVystaveni,
        DateTime? platnostDo = null,
        DateTime? datumVytvoreni = null)
    {
        Pacient = pacient;
        ZdravotnickyPracovnik = zdravotnickyPracovnik;
        PoskytovatelZdravotnickychSluzeb = poskytovatelZdravotnickychSluzeb;
        OdbornostLekare = odbornostLekare;
        TypAkce = typAkce;
        StavPosudku = stavPosudku;
        DruhProhlidky = druhProhlidky;
        DruhPosudku = druhPosudku;
        DatumVystaveni = datumVystaveni;
        PlatnostDo = platnostDo;

        DatumVytvoreni = datumVytvoreni ?? DateTime.UtcNow;

        VerzeZaznamu = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }

    public void AktualizovatStav(CiselnikPolozkaReference novyStav)
    {
        StavPosudku = novyStav;
        VerzeZaznamu = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}

public class PosudekRo
{
    public Guid Id { get; private set; }

    public PosudekHlavicka Hlavicka { get; private set; } = null!;

    private readonly List<PosudekZpusobilost> _zpusobilosti = [];
    public IReadOnlyCollection<PosudekZpusobilost> Zpusobilosti => _zpusobilosti.AsReadOnly();

    private readonly List<PosudekHistorie> _historie = [];
    public IReadOnlyCollection<PosudekHistorie> Historie => _historie.AsReadOnly();

    protected PosudekRo() { }

    public PosudekRo(PosudekHlavicka hlavicka)
    {
        Id = Guid.NewGuid();
        Hlavicka = hlavicka;
    }

    public void AddZpusobilost(PosudekZpusobilost zpusobilost)
    {
        zpusobilost.SetPosudekId(Id);
        _zpusobilosti.Add(zpusobilost);
    }

    public void AddHistoryRecord(PosudekHistorie historyRecord)
    {
        historyRecord.SetPosudekId(Id);
        _historie.Add(historyRecord);
    }

    public void Zneplatnit(CiselnikPolozkaReference newStav)
    {
        Hlavicka.AktualizovatStav(newStav);
    }
}