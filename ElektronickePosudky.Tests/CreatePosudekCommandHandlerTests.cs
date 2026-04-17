using AutoMapper;
using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Commands;
using ElektronickePosudky.Application.Repositories;
using ElektronickePosudky.Domain.Entities.PosudekAggregate;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ElektronickePosudky.Tests;

public class CreatePosudekCommandHandlerTests
{
    private readonly Mock<IPosudekRepository> _mockRepository;
    private readonly Mock<ILogger<CreatePosudekCommandHandler>> _mockLogger;
    private readonly Mock<IMapper> _mockMapper;

    private readonly Mock<ITransaction> _mockTransaction;

    private readonly CreatePosudekCommandHandler _handler;

    public CreatePosudekCommandHandlerTests()
    {
        _mockRepository = new Mock<IPosudekRepository>();
        _mockLogger = new Mock<ILogger<CreatePosudekCommandHandler>>();
        _mockMapper = new Mock<IMapper>();

        _mockTransaction = new Mock<ITransaction>();

        _mockRepository
            .Setup(repo => repo.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_mockTransaction.Object);

        _handler = new CreatePosudekCommandHandler(
            _mockRepository.Object,
            _mockLogger.Object,
            _mockMapper.Object
        );
    }

    [Fact]
    public async Task Handle_ValidRequest_CreatesPosudek_CommitsTransaction_AndReturnsDto()
    {
        var dummyData = new PosudekRoCreateDto
        {
            Rid = "123456789",
            KrzpId = "KRZP-001",
            StavPosudku = new CodebookItemDto { Kod = "STAV1", Verze = "1" },
            DruhProhlidky = new CodebookItemDto { Kod = "DRUH1", Verze = "1" },
            DruhPosudku = new CodebookItemDto { Kod = "POS1", Verze = "1" },

            Zpusobilosti = new List<PosudekZpusobilostDto>()
        };
        var command = new CreatePosudekCommand(dummyData, "corr-123");

        _mockMapper
            .Setup(m => m.Map<PosudekHlavickaResponseDto>(It.IsAny<object>()))
            .Returns(new PosudekHlavickaResponseDto());

        _mockMapper
            .Setup(m => m.Map<List<PosudekZpusobilostResponseDto>>(It.IsAny<object>()))
            .Returns(new List<PosudekZpusobilostResponseDto>());

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull("because the handler must return a valid DTO on success");
        result.Id.Should().NotBeEmpty("because a newly created Posudek must have a valid GUID");

        result.Hlavicka.Should().NotBeNull();
        result.Zpusobilosti.Should().NotBeNull();

        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<PosudekRo>(), It.IsAny<CancellationToken>()), Times.Once);
        _mockRepository.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        _mockTransaction.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockTransaction.Verify(t => t.RollbackAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_RollsBackTransaction_ThrowsException()
    {
        var dummyData = new PosudekRoCreateDto
        {
            Rid = "123456789",
            StavPosudku = new CodebookItemDto(),
            DruhProhlidky = new CodebookItemDto(),
            DruhPosudku = new CodebookItemDto()
        };
        var command = new CreatePosudekCommand(dummyData, "corr-123");

        _mockRepository
            .Setup(repo => repo.AddAsync(It.IsAny<PosudekRo>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database connection failed"));

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Database connection failed", "because this is the exception thrown by the Repository");

        _mockTransaction.Verify(t => t.RollbackAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockTransaction.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}