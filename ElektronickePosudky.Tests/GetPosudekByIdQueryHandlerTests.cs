using AutoMapper;
using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Queries;
using ElektronickePosudky.Application.Repositories;
using ElektronickePosudky.Domain.Entities.PosudekAggregate;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ElektronickePosudky.Tests.Features.Posudky.Queries;

public class GetPosudekByIdQueryHandlerTests
{
    private readonly Mock<IPosudekRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<GetPosudekByIdQueryHandler>> _mockLogger;
    private readonly GetPosudekByIdQueryHandler _handler;

    public GetPosudekByIdQueryHandlerTests()
    {
        _mockRepository = new Mock<IPosudekRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<GetPosudekByIdQueryHandler>>();

        _handler = new GetPosudekByIdQueryHandler(
            _mockRepository.Object,
            _mockMapper.Object,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task Handle_PosudekExists_ReturnsMappedDto()
    {
        var posudekId = Guid.NewGuid();
        var query = new GetPosudekByIdQuery(posudekId, "corr-123");

        var dummyPosudek = new Mock<PosudekRo>().Object;

        var expectedDto = new PosudekRoDetailDto { Id = posudekId };

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(posudekId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(dummyPosudek);

        _mockMapper
            .Setup(m => m.Map<PosudekRoDetailDto>(dummyPosudek))
            .Returns(expectedDto);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull("because the repository found the entity and it should be mapped");
        result.Should().BeEquivalentTo(expectedDto, "because the handler must return the mapped DTO");

        _mockRepository.Verify(repo => repo.GetByIdAsync(posudekId, It.IsAny<CancellationToken>()), Times.Once);
        _mockMapper.Verify(m => m.Map<PosudekRoDetailDto>(dummyPosudek), Times.Once);
    }

    [Fact]
    public async Task Handle_PosudekDoesNotExist_ReturnsNull()
    {
        var posudekId = Guid.NewGuid();
        var query = new GetPosudekByIdQuery(posudekId, "corr-123");

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(posudekId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((PosudekRo)null!);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeNull("because the entity was not found in the database");

        _mockRepository.Verify(repo => repo.GetByIdAsync(posudekId, It.IsAny<CancellationToken>()), Times.Once);

        _mockMapper.Verify(m => m.Map<PosudekRoDetailDto>(It.IsAny<PosudekRo>()), Times.Never);
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_ThrowsException()
    {
        var posudekId = Guid.NewGuid();
        var query = new GetPosudekByIdQuery(posudekId, "corr-123");

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(posudekId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database connection failed"));

        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Database connection failed", "because exceptions from the repository should bubble up");

        _mockMapper.Verify(m => m.Map<PosudekRoDetailDto>(It.IsAny<PosudekRo>()), Times.Never);
    }
}