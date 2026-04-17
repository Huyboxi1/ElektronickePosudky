using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using FluentAssertions;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class CodebookItemDtoValidatorTests
{
    private readonly CodebookItemDtoValidator _validator = new();

    [Fact]
    public void Should_Have_Errors_When_Fields_Are_Empty()
    {
        var dto = new CodebookItemDto { Kod = "", Verze = "" };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == "CodebookKodRequired");
        result.Errors.Should().Contain(e => e.ErrorMessage == "CodebookVerzeRequired");
    }

    [Fact]
    public void Should_Be_Valid_When_Fields_Are_Filled()
    {
        var dto = new CodebookItemDto { Kod = "TEST_KOD", Verze = "1.0" };
        var result = _validator.Validate(dto);

        result.IsValid.Should().BeTrue();
    }
}