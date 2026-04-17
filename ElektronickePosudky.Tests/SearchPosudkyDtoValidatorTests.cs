using System;
using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using FluentAssertions;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class SearchPosudkyDtoValidatorTests
{
    private readonly SearchPosudkyDtoValidator _validator = new();

    [Fact]
    public void Should_Be_Valid_With_Default_Pagination()
    {
        var dto = new SearchPosudkyDto { Page = 1, Size = 10 };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Page_Should_Have_Error_When_Less_Than_One(int invalidPage)
    {
        var dto = new SearchPosudkyDto { Page = invalidPage, Size = 10 };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e =>
            e.PropertyName == nameof(dto.Page) &&
            e.ErrorMessage == "PageInvalid");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    public void Size_Should_Have_Error_When_Out_Of_Range(int invalidSize)
    {
        var dto = new SearchPosudkyDto { Page = 1, Size = invalidSize };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e =>
            e.PropertyName == nameof(dto.Size) &&
            e.ErrorMessage == "SizeInvalid");
    }

    [Fact]
    public void Rid_Should_Have_Error_When_Exceeds_50_Characters()
    {
        var dto = new SearchPosudkyDto
        {
            Page = 1,
            Size = 10,
            Rid = new string('A', 51)
        };

        var result = _validator.Validate(dto);

        result.Errors.Should().Contain(e => e.ErrorMessage == "RidTooLong");
    }

    [Fact]
    public void Ico_Should_Have_Error_When_Exceeds_8_Characters()
    {
        var dto = new SearchPosudkyDto
        {
            Page = 1,
            Size = 10,
            Ico = "123456789"
        };

        var result = _validator.Validate(dto);

        result.Errors.Should().Contain(e => e.ErrorMessage == "IcoInvalid");
    }

    [Fact]
    public void Dates_Should_Have_Error_When_DatumDo_Is_Before_DatumOd()
    {
        var dto = new SearchPosudkyDto
        {
            Page = 1,
            Size = 10,
            DatumOd = new DateTime(2023, 01, 10),
            DatumDo = new DateTime(2023, 01, 01)
        };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e =>
            e.PropertyName == nameof(dto.DatumDo) &&
            e.ErrorMessage == "DatumDoInvalid");
    }

    [Fact]
    public void Dates_Should_Be_Valid_When_Only_One_Date_Is_Provided()
    {
        var dto = new SearchPosudkyDto
        {
            Page = 1,
            Size = 10,
            DatumOd = DateTime.Now,
            DatumDo = null
        };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeTrue("because cross-field validation shouldn't fire if one date is missing");
    }
}