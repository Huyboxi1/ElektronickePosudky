using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using FluentAssertions;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class PosudekZneplatnitDtoValidatorTests
{
    private readonly PosudekZneplatnitDtoValidator _validator;

    public PosudekZneplatnitDtoValidatorTests()
    {
        _validator = new PosudekZneplatnitDtoValidator();
    }

    [Fact]
    public void Validate_AllFieldsValid_ShouldNotHaveValidationErrors()
    {
        var dto = new PosudekZneplatnitDto
        {
            KrzpId = "KRZP-001",
            Ico = "12345678",
            DuvodZneplatneni = new CodebookItemDto { Kod = "ERROR_ENTRY", Verze = "1" }
        };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validate_InvalidKrzpId_ShouldHaveError(string? invalidKrzpId)
    {
        var dto = new PosudekZneplatnitDto { KrzpId = invalidKrzpId! };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.KrzpId));
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validate_InvalidIco_ShouldHaveError(string? invalidIco)
    {
        var dto = new PosudekZneplatnitDto { Ico = invalidIco! };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.Ico));
    }

    [Fact]
    public void Validate_NullDuvodZneplatneni_ShouldHaveError()
    {
        var dto = new PosudekZneplatnitDto
        {
            KrzpId = "KRZP1",
            Ico = "123",
            DuvodZneplatneni = null!
        };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(dto.DuvodZneplatneni));
    }

    [Fact]
    public void Validate_InvalidNestedDuvod_ShouldHaveNestedErrors()
    {
        var dto = new PosudekZneplatnitDto
        {
            KrzpId = "KRZP-001",
            Ico = "12345678",
            DuvodZneplatneni = new CodebookItemDto { Kod = "" }
        };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName.Contains("DuvodZneplatneni.Kod"));
    }
}