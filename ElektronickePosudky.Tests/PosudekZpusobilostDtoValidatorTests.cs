using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Validators;
using ElektronickePosudky.Application.Repositories;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Validators;

public class PosudekZpusobilostCreateDtoValidatorTests
{
    private readonly Mock<ICiselnikRepository> _mockRepo;
    private readonly PosudekZpusobilostCreateDtoValidator _validator;

    public PosudekZpusobilostCreateDtoValidatorTests()
    {
        _mockRepo = new Mock<ICiselnikRepository>();

        _mockRepo.Setup(x => x.PolozkaExistsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(true);

        _validator = new PosudekZpusobilostCreateDtoValidator(_mockRepo.Object);
    }

    [Fact]
    public async Task Should_Fail_If_SkupinyRidicskehoOpravneni_Is_Empty()
    {
        var dto = new PosudekZpusobilostCreateDto
        {
            SkupinaZadateleRidicKod = "skupina_ro_1",
            VysledekKod = "vysledek_posudku_ro_1",
            SkupinyRidicskehoOpravneniKody = new List<string>()
        };

        var result = await _validator.ValidateAsync(dto);

        result.Errors.Should().Contain(e => e.ErrorMessage == "SkupinyRidicskehoOpravneniRequired");
    }

    [Fact]
    public async Task Should_Trigger_Nested_HarmonizovaneKody_Validator()
    {
        var dto = new PosudekZpusobilostCreateDto
        {
            SkupinaZadateleRidicKod = "skupina_ro_1",
            VysledekKod = "vysledek_posudku_ro_1",
            SkupinyRidicskehoOpravneniKody = new List<string> { "B" },
            HarmonizovaneKody = new List<HarmonizovanyKodCreateDto>
            {
                new HarmonizovanyKodCreateDto { HarmonizovanyKod = "" }
            }
        };

        var result = await _validator.ValidateAsync(dto);

        result.Errors.Should().Contain(e => e.ErrorMessage == "HarmonizovanyKodRequired");
    }
}