namespace ElektronickePosudky.Domain.ValueObjects;

public record ZdravotnickyPracovnikVO(
    string KrzpId,
    string? TitulPred,
    string? Jmeno,
    string? Prijmeni,
    string? TitulZa,
    string? Odbornost
);
