namespace ElektronickePosudky.Application.DTOs;

public class PosudekZneplatnitDto
{
    public string KrzpId { get; set; } = string.Empty;
    public string Ico { get; set; } = string.Empty;

    public CodebookItemDto DuvodZneplatneni { get; set; } = null!;
}