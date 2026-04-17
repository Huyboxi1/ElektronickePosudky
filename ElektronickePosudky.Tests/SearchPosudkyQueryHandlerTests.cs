using AutoMapper;
using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Queries;
using ElektronickePosudky.Application.Repositories;
using ElektronickePosudky.Domain.Entities.PosudekAggregate;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ElektronickePosudky.Tests;

public class SearchPosudkyQueryHandlerTests
{
    private readonly Mock<IPosudekRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<SearchPosudkyQueryHandler>> _mockLogger;
    private readonly SearchPosudkyQueryHandler _handler;

    public SearchPosudkyQueryHandlerTests()
    {
        _mockRepository = new Mock<IPosudekRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<SearchPosudkyQueryHandler>>();

        _handler = new SearchPosudkyQueryHandler(
            _mockRepository.Object,
            _mockMapper.Object,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task Handle_HasMoreResults_ReturnsCorrectNextPage()
    {
        var searchDto = new SearchPosudkyDto { Page = 1, Size = 10 };
        var query = new SearchPosudkyQuery(searchDto, "corr-123");

        var domainList = new List<PosudekRo> { new Mock<PosudekRo>().Object, new Mock<PosudekRo>().Object };
        var dtoList = new List<PosudekRoDetailDto> { new PosudekRoDetailDto(), new PosudekRoDetailDto() };
        var totalCount = 15;

        SetupRepositoryAndMapper(domainList, totalCount, dtoList);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.TotalCount.Should().Be(15);
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
        result.NextPage.Should().Be(2, "because (Page * Size) [10] < TotalCount [15], there is a next page");
        result.Page.Should().BeEquivalentTo(dtoList);
    }

    [Fact]
    public async Task Handle_LastPage_ReturnsNextPageAsZero()
    {
        var searchDto = new SearchPosudkyDto { Page = 2, Size = 10 };
        var query = new SearchPosudkyQuery(searchDto, "corr-123");

        var domainList = new List<PosudekRo> { new Mock<PosudekRo>().Object };
        var dtoList = new List<PosudekRoDetailDto> { new PosudekRoDetailDto() };
        var totalCount = 15;

        SetupRepositoryAndMapper(domainList, totalCount, dtoList);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.TotalCount.Should().Be(15);
        result.PageNumber.Should().Be(2);
        result.PageSize.Should().Be(10);
        result.NextPage.Should().Be(0, "because (Page * Size) [20] >= TotalCount [15], there are no more pages");
        result.Page.Should().BeEquivalentTo(dtoList);
    }

    [Fact]
    public async Task Handle_NoResultsFound_ReturnsEmptyPageAndZeroTotal()
    {
        var searchDto = new SearchPosudkyDto { Page = 1, Size = 10 };
        var query = new SearchPosudkyQuery(searchDto, "corr-123");

        var domainList = new List<PosudekRo>();
        var dtoList = new List<PosudekRoDetailDto>();
        var totalCount = 0;

        SetupRepositoryAndMapper(domainList, totalCount, dtoList);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.TotalCount.Should().Be(0);
        result.NextPage.Should().Be(0, "because there are no records");
        result.Page.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_ThrowsException()
    {
        var query = new SearchPosudkyQuery(new SearchPosudkyDto(), "corr-123");

        _mockRepository
            .Setup(repo => repo.SearchAsync(
                It.IsAny<string?>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<bool?>(),
                It.IsAny<Guid?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database connection timeout"));

        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Database connection timeout");
    }

    private void SetupRepositoryAndMapper(List<PosudekRo> domainList, int totalCount, List<PosudekRoDetailDto> dtoList)
    {
        _mockRepository
            .Setup(repo => repo.SearchAsync(
                It.IsAny<string?>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<bool?>(),
                It.IsAny<Guid?>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<int>(),
                It.IsAny<int>(), It.IsAny<string?>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((domainList, totalCount));

        _mockMapper
            .Setup(m => m.Map<List<PosudekRoDetailDto>>(domainList))
            .Returns(dtoList);
    }
}