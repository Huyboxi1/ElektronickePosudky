using System;
using System.Linq;
using ElektronickePosudky.Application.Features.Posudky.Queries;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using FluentAssertions;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class GetPosudekHistoryQueryValidatorTests
{
    private readonly GetPosudekHistoryQueryValidator _validator;

    public GetPosudekHistoryQueryValidatorTests()
    {
        _validator = new GetPosudekHistoryQueryValidator();
    }

    [Fact]
    public void Handle_ValidId_ShouldNotHaveValidationErrors()
    {
        var query = new GetPosudekHistoryQuery(Guid.NewGuid(), "corr-123");

        var result = _validator.Validate(query);

        result.IsValid.Should().BeTrue("because the record ID is a valid Guid");
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Handle_EmptyId_ShouldHaveRequiredError()
    {
        var query = new GetPosudekHistoryQuery(Guid.Empty, "corr-123");

        var result = _validator.Validate(query);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().ContainSingle()
            .Which.ErrorMessage.Should().Be("PosudekIdRequired");

        result.Errors.Should().Contain(e => e.PropertyName == "Id");
    }

    [Fact]
    public void Handle_DefaultGuid_ShouldBeInvalid()
    {
        var query = new GetPosudekHistoryQuery(default, "corr-123");

        var result = _validator.Validate(query);

        result.IsValid.Should().BeFalse("because default(Guid) is equivalent to Guid.Empty");
        result.Errors.Select(x => x.ErrorMessage).Should().Contain("PosudekIdRequired");
    }
}