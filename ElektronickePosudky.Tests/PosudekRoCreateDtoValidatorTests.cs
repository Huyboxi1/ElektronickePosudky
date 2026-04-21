using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using ElektronickePosudky.Application.Repositories;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class PosudekRoCreateDtoValidatorTests
{
    private readonly Mock<ICiselnikRepository> _mockRepo;
    private readonly PosudekRoCreateDtoValidator _validator;

    public PosudekRoCreateDtoValidatorTests()
    {
        _mockRepo = new Mock<ICiselnikRepository>();

        _mockRepo.Setup(x => x.PolozkaExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(true);

        _validator = new PosudekRoCreateDtoValidator(_mockRepo.Object);
    }

    private PosudekRoCreateDto GetValidDto() => new()
    {
        Rid = "1234567890",
        KrzpId = "KRZP-001",
        DatumVystaveni = DateTime.Now.AddDays(-1),
        TypAkceKod = "akce_ro_1",
        StavPosudkuKod = "stav_posudku_1",
        DruhProhlidkyKod = "druh_prohlidky_ro_1",
        DruhPosudkuKod = "druh_posudku_ro_1",
        Zpusobilosti = new List<PosudekZpusobilostCreateDto>
        {
            new() {
                SkupinaZadateleRidicKod = "skupina_ro_1",
                VysledekKod = "vysledek_posudku_ro_1",
                SkupinyRidicskehoOpravneniKody = new List<string> { "B" }
            }
        }
    };

    [Theory]
    [InlineData("123")]
    [InlineData("12345678901")]
    public async Task Rid_Should_Have_Error_If_Length_Not_10(string invalidRid)
    {
        var dto = GetValidDto();
        dto.Rid = invalidRid;

        var result = await _validator.TestValidateAsync(dto);

        result.ShouldHaveValidationErrorFor(x => x.Rid)
              .WithErrorMessage("RidInvalidLength");
    }

    [Fact]
    public async Task DatumVystaveni_Should_Fail_If_In_Future()
    {
        var dto = GetValidDto();
        dto.DatumVystaveni = DateTime.Now.AddDays(1);

        var result = await _validator.TestValidateAsync(dto);

        result.ShouldHaveValidationErrorFor(x => x.DatumVystaveni)
              .WithErrorMessage("DatumVystaveniFuture");
    }

    [Fact]
    public async Task PlatnostDo_Should_Fail_If_Earlier_Than_DatumVystaveni()
    {
        var dto = GetValidDto();
        dto.DatumVystaveni = DateTime.Now;
        dto.PlatnostDo = DateTime.Now.AddDays(-1);

        var result = await _validator.TestValidateAsync(dto);

        result.ShouldHaveValidationErrorFor(x => x.PlatnostDo)
              .WithErrorMessage("PlatnostDoInvalid");
    }

    [Fact]
    public async Task Should_Fail_If_Zpusobilosti_Is_Empty()
    {
        var dto = GetValidDto();
        dto.Zpusobilosti = new List<PosudekZpusobilostCreateDto>();

        var result = await _validator.TestValidateAsync(dto);

        result.ShouldHaveValidationErrorFor(x => x.Zpusobilosti)
              .WithErrorMessage("ZpusobilostiRequired");
    }

    [Fact]
    public async Task Should_Have_Nested_Errors_If_Zpusobilost_Items_Are_Invalid()
    {
        var dto = GetValidDto();
        dto.Zpusobilosti[0].SkupinaZadateleRidicKod = string.Empty;

        var result = await _validator.TestValidateAsync(dto);

        result.ShouldHaveValidationErrorFor("Zpusobilosti[0].SkupinaZadateleRidicKod")
              .WithErrorMessage("SkupinaZadateleRidicRequired");
    }
}