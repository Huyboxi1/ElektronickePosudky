using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Commands;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class CreatePosudekCommandValidatorTests
{
    private readonly CreatePosudekCommandValidator _validator;

    public CreatePosudekCommandValidatorTests()
    {
        _validator = new CreatePosudekCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Data_Is_Null()
    {
        var command = new CreatePosudekCommand(null!, "corr-123");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Data)
              .WithErrorMessage("'Data' must not be empty.");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Data_Is_Provided()
    {
        var dto = new PosudekRoCreateDto
        {
            Rid = "123456789",
            KrzpId = "KRZP-001"
        };
        var command = new CreatePosudekCommand(dto, "corr-123");

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Data);
    }

    [Fact]
    public void Should_Have_Nested_Errors_When_Dto_Is_Invalid()
    {
        var dto = new PosudekRoCreateDto
        {
            Rid = "",
            KrzpId = ""
        };
        var command = new CreatePosudekCommand(dto, "corr-123");

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor("Data.Rid");
        result.ShouldHaveValidationErrorFor("Data.KrzpId");
    }
}