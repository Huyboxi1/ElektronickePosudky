using System;
using System.Linq;
using ElektronickePosudky.Application.Features.Posudky.Queries;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using FluentAssertions;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class GetPosudekPdfQueryValidatorTests
{
    private readonly GetPosudekPdfQueryValidator _validator;

    public GetPosudekPdfQueryValidatorTests()
    {
        _validator = new GetPosudekPdfQueryValidator();
    }

    [Fact]
    public void Handle_ValidId_ShouldNotHaveValidationErrors()
    {
        var query = new GetPosudekPdfQuery(Guid.NewGuid(), "corr-pdf-123");

        var result = _validator.Validate(query);

        result.IsValid.Should().BeTrue("because a valid Guid was provided for the PDF request");
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Handle_EmptyId_ShouldHaveRequiredError()
    {
        var query = new GetPosudekPdfQuery(Guid.Empty, "corr-pdf-123");

        var result = _validator.Validate(query);

        result.IsValid.Should().BeFalse("because the PDF cannot be generated without a specific Posudek ID");

        result.Errors.Should().ContainSingle()
            .Which.ErrorMessage.Should().Be("PosudekIdRequired");

        result.Errors.Should().Contain(e => e.PropertyName == "Id");
    }

    [Fact]
    public void Handle_DefaultGuid_ShouldBeInvalid()
    {
        var query = new GetPosudekPdfQuery(default, "corr-pdf-123");

        var result = _validator.Validate(query);

        result.IsValid.Should().BeFalse();
        result.Errors.Select(x => x.ErrorMessage).Should().Contain("PosudekIdRequired");
    }
}