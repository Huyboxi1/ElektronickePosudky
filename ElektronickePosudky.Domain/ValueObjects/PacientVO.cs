namespace ElektronickePosudky.Domain.ValueObjects;

public record PacientVO(
    string Rid,
    string? Jmeno,
    string? Prijmeni,
    DateTime DatumNarozeni,
    string? Adresa,
    string? Email,
    string? Doklad,
    string? Pohlavi
);
