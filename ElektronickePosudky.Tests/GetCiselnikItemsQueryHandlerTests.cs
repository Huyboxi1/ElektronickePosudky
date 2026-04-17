using AutoMapper;
using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Ciselniky.Queries;
using ElektronickePosudky.Application.Repositories;
using ElektronickePosudky.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ElektronickePosudky.Tests;

public class GetCiselnikItemsQueryHandlerTests
{
    private readonly Mock<ICiselnikRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<GetCiselnikItemsQueryHandler>> _mockLogger;
    private readonly GetCiselnikItemsQueryHandler _handler;

    public GetCiselnikItemsQueryHandlerTests()
    {
        _mockRepository = new Mock<ICiselnikRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<GetCiselnikItemsQueryHandler>>();

        _handler = new GetCiselnikItemsQueryHandler(
            _mockRepository.Object,
            _mockMapper.Object,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task Handle_ReturnsMappedList_WhenItemsExistForGivenCode()
    {
        var testCode = "DRUH_POSUDKU";
        var query = new GetCiselnikItemsQuery(testCode, "corr-123");

        var domainList = new List<CiselnikPolozka>
        {
            new Mock<CiselnikPolozka>().Object,
            new Mock<CiselnikPolozka>().Object
        };

        var dtoList = new List<CiselnikPolozkaDto>
        {
            new CiselnikPolozkaDto { Id = Guid.NewGuid(), Kod = "ITEM1" },
            new CiselnikPolozkaDto { Id = Guid.NewGuid(), Kod = "ITEM2" }
        };

        _mockRepository
            .Setup(repo => repo.GetItemsByCodeAsync(testCode, It.IsAny<CancellationToken>()))
            .ReturnsAsync(domainList);

        _mockMapper
            .Setup(m => m.Map<List<CiselnikPolozkaDto>>(domainList))
            .Returns(dtoList);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull("because the handler should always return a list, even if empty");
        result.Should().HaveCount(2, "because the repository returned 2 items");
        result.Should().BeEquivalentTo(dtoList, "because the result must match the mapped DTOs");

        _mockRepository.Verify(repo => repo.GetItemsByCodeAsync(testCode, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoItemsExistForGivenCode()
    {
        // Arrange
        var testCode = "UNKNOWN_CODE";
        var query = new GetCiselnikItemsQuery(testCode, "corr-123");

        var emptyDomainList = new List<CiselnikPolozka>();
        var emptyDtoList = new List<CiselnikPolozkaDto>();

        _mockRepository
            .Setup(repo => repo.GetItemsByCodeAsync(testCode, It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyDomainList);

        _mockMapper
            .Setup(m => m.Map<List<CiselnikPolozkaDto>>(emptyDomainList))
            .Returns(emptyDtoList);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEmpty("because the repository returned no items for the given code");

        _mockRepository.Verify(repo => repo.GetItemsByCodeAsync(testCode, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenRepositoryFails()
    {
        var testCode = "DRUH_POSUDKU";
        var query = new GetCiselnikItemsQuery(testCode, "corr-123");

        _mockRepository
            .Setup(repo => repo.GetItemsByCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database timeout"));

        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Database timeout", "because the handler does not swallow exceptions and should let them bubble up");

        _mockMapper.Verify(m => m.Map<List<CiselnikPolozkaDto>>(It.IsAny<List<CiselnikPolozka>>()), Times.Never);
    }
}