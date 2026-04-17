namespace ElektronickePosudky.Application.DTOs;

public class PosudekHistorieDto
{
    public CiselnikPolozkaReferenceDto TypOperace { get; set; } = null!;
    public DateTime DatumOperace { get; set; }
    public ZdravotnickyPracovnikDetailDto Lekar { get; set; } = null!;
    public PoskytovatelDetailDto Poskytovatel { get; set; } = null!;
}