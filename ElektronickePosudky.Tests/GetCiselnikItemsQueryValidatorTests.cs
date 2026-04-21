using ElektronickePosudky.Application.Features.Ciselniky.Queries;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using FluentAssertions;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class GetCiselnikItemsQueryValidatorTests
{
    private readonly GetCiselnikItemsQueryValidator _validator;

    public GetCiselnikItemsQueryValidatorTests()
    {
        _validator = new GetCiselnikItemsQueryValidator();
    }

    [Fact]
    public void Handle_ValidKod_ShouldNotHaveValidationErrors()
    {
        var query = new GetCiselnikItemsQuery("1234567890", "corr-123");

        var result = _validator.Validate(query);

        result.IsValid.Should().BeTrue("because the codebook has exactly 10 characters.");
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Handle_EmptyKod_ShouldHaveRequiredError(string? invalidKod)
    {
        // Arrange
        var query = new GetCiselnikItemsQuery(invalidKod!, "corr-123");

        // Act
        var result = _validator.Validate(query);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle()
            .Which.ErrorMessage.Should().Be("CodebookKodRequired");
    }


    [Fact]
    public void Handle_WhitespaceKod_ShouldBeInvalid()
    {
        var query = new GetCiselnikItemsQuery("          ", "corr-123");

        var result = _validator.Validate(query);

        result.IsValid.Should().BeFalse();
        result.Errors.Select(x => x.ErrorMessage).Should().Contain("CodebookKodRequired");
    }
}