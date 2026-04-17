using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Queries;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using FluentAssertions;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class SearchPosudkyQueryValidatorTests
{
    private readonly SearchPosudkyQueryValidator _validator;

    public SearchPosudkyQueryValidatorTests()
    {
        _validator = new SearchPosudkyQueryValidator();
    }

    [Fact]
    public void Validate_WhenDataIsNull_ShouldHaveValidationError()
    {
        var query = new SearchPosudkyQuery(null!, "corr-789");

        var result = _validator.Validate(query);

        result.IsValid.Should().BeFalse("because the Data DTO is required for searching");
        result.Errors.Should().Contain(e => e.PropertyName == nameof(query.Data));
    }

    [Fact]
    public void Validate_WhenDataIsProvided_ShouldNotHaveTopLevelError()
    {
        var validData = new SearchPosudkyDto
        {
            Page = 1,
            Size = 10
        };
        var query = new SearchPosudkyQuery(validData, "corr-789");

        var result = _validator.Validate(query);
        result.Errors.Should().NotContain(e => e.PropertyName == nameof(query.Data));
    }

    [Fact]
    public void Validate_WhenNestedDataIsInvalid_ShouldTriggerNestedValidator()
    {
        var invalidData = new SearchPosudkyDto
        {
            Page = 1,
            Size = 999
        };
        var query = new SearchPosudkyQuery(invalidData, "corr-789");

        var result = _validator.Validate(query);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(e =>
            e.PropertyName == "Data.Size" &&
            e.ErrorMessage == "SizeInvalid");
    }

    [Fact]
    public void Validate_WhenEverythingIsValid_ShouldBeValid()
    {
        var data = new SearchPosudkyDto { Page = 1, Size = 20 };
        var query = new SearchPosudkyQuery(data, "corr-789");

        var result = _validator.Validate(query);

        result.IsValid.Should().BeTrue();
    }
}