using System.Collections.Generic;

namespace ElektronickePosudky.Application.DTOs;

public class CiselnikDto
{
    public Guid Id { get; set; }
    public string Kod { get; set; } = string.Empty;
    public string Verze { get; set; } = string.Empty;
    public DateTime PlatnostOd { get; set; }
    public DateTime? PlatnostDo { get; set; }
    public bool Termx { get; set; }
    public string? TermxId { get; set; }
    public string? TermxUrl { get; set; }
    public Dictionary<string, TranslationItemDto> Preklady { get; set; } = new();
}