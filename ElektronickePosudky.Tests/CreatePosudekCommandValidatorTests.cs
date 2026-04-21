using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Commands;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using ElektronickePosudky.Application.Repositories;
using FluentValidation.TestHelper;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class CreatePosudekCommandValidatorTests
{
    private readonly Mock<ICiselnikRepository> _mockRepo;
    private readonly CreatePosudekCommandValidator _validator;

    public CreatePosudekCommandValidatorTests()
    {
        _mockRepo = new Mock<ICiselnikRepository>();

        _mockRepo.Setup(x => x.PolozkaExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(true);

        _validator = new CreatePosudekCommandValidator(_mockRepo.Object);
    }

    [Fact]
    public async Task Should_Have_Error_When_Data_Is_Null()
    {
        var command = new CreatePosudekCommand(null!, "corr-123");

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(x => x.Data)
              .WithErrorMessage("data is required.");
    }

    [Fact]
    public async Task Should_Not_Have_Error_When_Data_Is_Provided()
    {
        var dto = new PosudekRoCreateDto
        {
            Rid = "1234567890",
            KrzpId = "KRZP-001"
        };
        var command = new CreatePosudekCommand(dto, "corr-123");

        var result = await _validator.TestValidateAsync(command);

        result.ShouldNotHaveValidationErrorFor(x => x.Data);
    }

    [Fact]
    public async Task Should_Have_Nested_Errors_When_Dto_Is_Invalid()
    {
        var dto = new PosudekRoCreateDto
        {
            Rid = "",
            KrzpId = ""
        };
        var command = new CreatePosudekCommand(dto, "corr-123");

        var result = await _validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor("Data.Rid");
        result.ShouldHaveValidationErrorFor("Data.KrzpId");
    }
}