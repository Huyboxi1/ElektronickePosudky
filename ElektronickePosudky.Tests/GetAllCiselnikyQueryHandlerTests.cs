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

public class GetAllCiselnikyQueryHandlerTests
{
    private readonly Mock<ICiselnikRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<GetAllCiselnikyQueryHandler>> _mockLogger;
    private readonly GetAllCiselnikyQueryHandler _handler;

    public GetAllCiselnikyQueryHandlerTests()
    {
        _mockRepository = new Mock<ICiselnikRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<GetAllCiselnikyQueryHandler>>();

        _handler = new GetAllCiselnikyQueryHandler(
            _mockRepository.Object,
            _mockMapper.Object,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task Handle_ReturnsMappedList_WhenCodebooksExist()
    {
        var query = new GetAllCiselnikyQuery("corr-123");

        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();

        var domainList = new List<Ciselnik>
        {
            new Ciselnik(id1, "CODE1", "1.0", DateTime.UtcNow, null, null),
            new Ciselnik(id2, "CODE2", "1.0", DateTime.UtcNow, null, null)
        };

        var dtoList = new List<CiselnikDto>
        {
            new CiselnikDto { Id = id1, Kod = "CODE1" },
            new CiselnikDto { Id = id2, Kod = "CODE2" }
        };

        _mockRepository
            .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(domainList);

        _mockMapper
            .Setup(m => m.Map<List<CiselnikDto>>(domainList))
            .Returns(dtoList);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull("because the handler should always return a list, even if empty");
        result.Should().HaveCount(2, "because the repository returned 2 items");
        result.Should().BeEquivalentTo(dtoList, "because the result must match the mapped DTOs");

        _mockRepository.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoCodebooksExist()
    {
        var query = new GetAllCiselnikyQuery("corr-123");
        var emptyDomainList = new List<Ciselnik>();
        var emptyDtoList = new List<CiselnikDto>();

        _mockRepository
            .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyDomainList);

        _mockMapper
            .Setup(m => m.Map<List<CiselnikDto>>(emptyDomainList))
            .Returns(emptyDtoList);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEmpty("because the repository returned no items");

        _mockRepository.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ThrowsException_WhenRepositoryFails()
    {
        var query = new GetAllCiselnikyQuery("corr-123");

        _mockRepository
            .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database timeout"));

        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Database timeout", "because the handler does not swallow exceptions and should let them bubble up");

        _mockMapper.Verify(m => m.Map<List<CiselnikDto>>(It.IsAny<List<Ciselnik>>()), Times.Never);
    }
}