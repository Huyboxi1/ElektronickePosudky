namespace ElektronickePosudky.Application.DTOs;

public class SearchPosudkyDto
{
    public string? Rid { get; set; }
    public DateTime? DatumOd { get; set; }
    public DateTime? DatumDo { get; set; }
    public bool? JenPlatne { get; set; }
    public Guid? StavPosudku { get; set; }
    public string? Fulltext { get; set; }
    public string? Ico { get; set; }
    public int Page { get; set; } = 1;
    public int Size { get; set; } = 10;
    public string? Sort { get; set; }
    public string? Order { get; set; }
}