using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElektronickePosudky.Domain.ValueObjects;

public record TranslationVO(
    string? Nazev,
    string? Popis
)
{
    private TranslationVO() : this(default, default)
    {
    }
}

public record CiselnikPolozkaReference(
    string CiselnikKod,
    string CiselnikVerze,
    string PolozkaKod,
    [property: NotMapped] Dictionary<string, TranslationVO> Preklady
)
{
    private CiselnikPolozkaReference() : this(default!, default!, default!, new Dictionary<string, TranslationVO>())
    {
    }
}