using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class PosudekZpusobilostDtoValidatorTests
{
    private readonly PosudekZpusobilostDtoValidator _validator = new();

    [Fact]
    public void Should_Fail_If_SkupinyRidicskehoOpravneni_Is_Empty()
    {
        var dto = new PosudekZpusobilostDto
        {
            SkupinaZadateleRidic = new CodebookItemDto { Kod = "A", Verze = "1" },
            Vysledek = new CodebookItemDto { Kod = "B", Verze = "1" },
            SkupinyRidicskehoOpravneni = new List<PosudekSkupinaRoDetailDto>() // Empty
        };

        var result = _validator.Validate(dto);

        result.Errors.Should().Contain(e => e.ErrorMessage == "SkupinyRidicskehoOpravneniRequired");
    }

    [Fact]
    public void Should_Trigger_Nested_HarmonizovaneKody_Validator()
    {
        var dto = new PosudekZpusobilostDto
        {
            SkupinaZadateleRidic = new CodebookItemDto { Kod = "A", Verze = "1" },
            Vysledek = new CodebookItemDto { Kod = "B", Verze = "1" },
            SkupinyRidicskehoOpravneni = new List<PosudekSkupinaRoDetailDto> {
                new() { SkupinaRo = new CodebookItemDto { Kod = "B", Verze = "1" } }
            },
            HarmonizovaneKody = new List<HarmonizovanyKodDetailDto>
            {
                new() { HarmonizovanyKod = null! }
            }
        };

        var result = _validator.Validate(dto);

        result.Errors.Should().Contain(e => e.ErrorMessage == "HarmonizovanyKodRequired");
    }
}