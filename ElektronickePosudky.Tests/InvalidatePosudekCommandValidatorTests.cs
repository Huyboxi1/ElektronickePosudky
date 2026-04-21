using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using ElektronickePosudky.Application.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class PosudekZneplatnitDtoValidatorTests
{
    private readonly Mock<ICiselnikRepository> _mockRepo;
    private readonly PosudekZneplatnitDtoValidator _validator;

    public PosudekZneplatnitDtoValidatorTests()
    {
        _mockRepo = new Mock<ICiselnikRepository>();

        _validator = new PosudekZneplatnitDtoValidator(_mockRepo.Object);
    }

    [Fact]
    public async Task Should_Have_Error_When_Codebook_Value_Is_Invalid()
    {
        _mockRepo.Setup(x => x.PolozkaExistsAsync("akce-ro", "akce_ro_999", It.IsAny<CancellationToken>()))
                 .ReturnsAsync(false);

        var dto = new PosudekZneplatnitDto
        {
            KrzpId = "DOC123",
            Ico = "12345678",
            DuvodZneplatneniKod = "akce_ro_999"
        };

        var result = await _validator.ValidateAsync(dto);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage == "InvalidCodebookValue");
    }

    [Fact]
    public async Task Should_Be_Valid_When_Codebook_Value_Exists()
    {
        _mockRepo.Setup(x => x.PolozkaExistsAsync("akce-ro", "akce_ro_3", It.IsAny<CancellationToken>()))
                 .ReturnsAsync(true);

        var dto = new PosudekZneplatnitDto
        {
            KrzpId = "DOC123",
            Ico = "12345678",
            DuvodZneplatneniKod = "akce_ro_3"
        };

        var result = await _validator.ValidateAsync(dto);

        result.IsValid.Should().BeTrue();
    }
}