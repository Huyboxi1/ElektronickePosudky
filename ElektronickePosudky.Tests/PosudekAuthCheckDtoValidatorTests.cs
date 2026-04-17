using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using FluentAssertions;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class PosudekAuthCheckDtoValidatorTests
{
    private readonly PosudekAuthCheckDtoValidator _validator;

    public PosudekAuthCheckDtoValidatorTests()
    {
        _validator = new PosudekAuthCheckDtoValidator();
    }

    [Fact]
    public void Validate_AllFieldsValid_ShouldNotHaveValidationErrors()
    {
        var dto = new PosudekAuthCheckDto
        {
            KrzpId = "KRZP-778899",
            Ico = "12345678"
        };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void Validate_KrzpIdEmpty_ShouldHaveRequiredError(string? invalidKrzpId)
    {
        var dto = new PosudekAuthCheckDto { KrzpId = invalidKrzpId!, Ico = "123" };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e =>
            e.PropertyName == nameof(dto.KrzpId) &&
            e.ErrorMessage == "KrzpIdRequired");
    }

    [Fact]
    public void Validate_KrzpIdTooLong_ShouldHaveLengthError()
    {
        var dto = new PosudekAuthCheckDto
        {
            KrzpId = new string('A', 51),
            Ico = "123"
        };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e =>
            e.PropertyName == nameof(dto.KrzpId) &&
            e.ErrorMessage == "KrzpIdTooLong");
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Validate_IcoEmpty_ShouldHaveRequiredError(string? invalidIco)
    {
        var dto = new PosudekAuthCheckDto { KrzpId = "ValidID", Ico = invalidIco! };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e =>
            e.PropertyName == nameof(dto.Ico) &&
            e.ErrorMessage == "IcoRequired");
    }

    [Fact]
    public void Validate_IcoTooLong_ShouldHaveLengthError()
    {
        var dto = new PosudekAuthCheckDto
        {
            KrzpId = "ValidID",
            Ico = new string('1', 21)
        };

        var result = _validator.Validate(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e =>
            e.PropertyName == nameof(dto.Ico) &&
            e.ErrorMessage == "IcoTooLong");
    }
}