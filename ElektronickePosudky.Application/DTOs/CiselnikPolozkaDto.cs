using System.Collections.Generic;

namespace ElektronickePosudky.Application.DTOs;

public class CiselnikPolozkaDto
{
    public Guid Id { get; set; }
    public string Kod { get; set; } = string.Empty;
    public string Verze { get; set; } = string.Empty;
    public Guid? RodicId { get; set; }
    public Dictionary<string, TranslationItemDto> Preklady { get; set; } = new();
}