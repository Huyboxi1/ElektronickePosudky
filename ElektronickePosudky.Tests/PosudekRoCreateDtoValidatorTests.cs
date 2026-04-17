using System;
using System.Collections.Generic;
using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using FluentAssertions;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class PosudekRoCreateDtoValidatorTests
{
    private readonly PosudekRoCreateDtoValidator _validator = new();

    private PosudekRoCreateDto GetValidDto() => new()
    {
        Rid = "1234567890",
        KrzpId = "KRZP-001",
        DatumVystaveni = DateTime.Now.AddDays(-1),
        TypAkce = new CodebookItemDto { Kod = "A", Verze = "1" },
        StavPosudku = new CodebookItemDto { Kod = "S", Verze = "1" },
        DruhProhlidky = new CodebookItemDto { Kod = "P", Verze = "1" },
        DruhPosudku = new CodebookItemDto { Kod = "D", Verze = "1" },
        Zpusobilosti = new List<PosudekZpusobilostDto>
        {
            new() {
                SkupinaZadateleRidic = new CodebookItemDto { Kod = "X", Verze = "1" },
                Vysledek = new CodebookItemDto { Kod = "Y", Verze = "1" },
                SkupinyRidicskehoOpravneni = new List<PosudekSkupinaRoDetailDto> {
                    new() { SkupinaRo = new CodebookItemDto { Kod = "B", Verze = "1" } }
                }
            }
        }
    };

    [Theory]
    [InlineData("123")]
    [InlineData("12345678901")]
    public void Rid_Should_Have_Error_If_Length_Not_10(string invalidRid)
    {
        var dto = GetValidDto();
        dto.Rid = invalidRid;

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == "RidInvalidLength");
    }

    [Fact]
    public void DatumVystaveni_Should_Fail_If_In_Future()
    {
        var dto = GetValidDto();
        dto.DatumVystaveni = DateTime.Now.AddDays(1);

        var result = _validator.Validate(dto);

        result.Errors.Should().Contain(e => e.ErrorMessage == "DatumVystaveniFuture");
    }

    [Fact]
    public void PlatnostDo_Should_Fail_If_Earlier_Than_DatumVystaveni()
    {
        var dto = GetValidDto();
        dto.DatumVystaveni = DateTime.Now;
        dto.PlatnostDo = DateTime.Now.AddDays(-1);

        var result = _validator.Validate(dto);

        result.Errors.Should().Contain(e => e.ErrorMessage == "PlatnostDoInvalid");
    }

    [Fact]
    public void Should_Fail_If_Zpusobilosti_Is_Empty()
    {
        var dto = GetValidDto();
        dto.Zpusobilosti = new List<PosudekZpusobilostDto>();

        var result = _validator.Validate(dto);

        result.Errors.Should().Contain(e => e.ErrorMessage == "ZpusobilostiRequired");
    }

    [Fact]
    public void Should_Have_Nested_Errors_If_Zpusobilost_Items_Are_Invalid()
    {
        var dto = GetValidDto();
        dto.Zpusobilosti[0].SkupinaZadateleRidic = null!;

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName.StartsWith("Zpusobilosti[0]"));
    }
}