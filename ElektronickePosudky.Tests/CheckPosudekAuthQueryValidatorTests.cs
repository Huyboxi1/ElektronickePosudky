using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Queries;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class CheckPosudekAuthQueryValidatorTests
{
    private readonly CheckPosudekAuthQueryValidator _validator;

    public CheckPosudekAuthQueryValidatorTests()
    {
        _validator = new CheckPosudekAuthQueryValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Data_Is_Null()
    {
        var query = new CheckPosudekAuthQuery(null!, "corr-123");

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Data)
              .WithErrorMessage("'Data' must not be empty.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Data_Is_Provided()
    {
        var dto = new PosudekAuthCheckDto
        {
            KrzpId = "KRZP123",
            Ico = "12345678"
        };
        var query = new CheckPosudekAuthQuery(dto, "corr-123");

        var result = _validator.TestValidate(query);
        result.ShouldNotHaveValidationErrorFor(x => x.Data);
    }

    [Fact]
    public void Should_Have_Errors_When_Nested_Dto_Is_Invalid()
    {
        var dto = new PosudekAuthCheckDto
        {
            KrzpId = "",
            Ico = ""
        };
        var query = new CheckPosudekAuthQuery(dto, "corr-123");

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor("Data.KrzpId");
        result.ShouldHaveValidationErrorFor("Data.Ico");
    }
}