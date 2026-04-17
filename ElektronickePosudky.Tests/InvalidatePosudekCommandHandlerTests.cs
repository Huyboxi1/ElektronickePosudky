using AutoMapper;
using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Commands;
using ElektronickePosudky.Application.Repositories;
using ElektronickePosudky.Domain.Entities.PosudekAggregate;
using ElektronickePosudky.Domain.ValueObjects;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Reflection;
using Xunit;

namespace ElektronickePosudky.Tests;

public class InvalidatePosudekCommandHandlerTests
{
    private readonly Mock<IPosudekRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<InvalidatePosudekCommandHandler>> _mockLogger;
    private readonly Mock<ITransaction> _mockTransaction;
    private readonly InvalidatePosudekCommandHandler _handler;

    public InvalidatePosudekCommandHandlerTests()
    {
        _mockRepository = new Mock<IPosudekRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<InvalidatePosudekCommandHandler>>();
        _mockTransaction = new Mock<ITransaction>();

        _mockRepository
            .Setup(repo => repo.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_mockTransaction.Object);

        _handler = new InvalidatePosudekCommandHandler(
            _mockRepository.Object,
            _mockMapper.Object,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task Handle_PosudekNotFound_ReturnsNull()
    {
        // Arrange
        var command = new InvalidatePosudekCommand(Guid.NewGuid(), "\"v1\"", new PosudekZneplatnitDto(), "corr-123");

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((PosudekRo)null!);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeNull("because the repository did not find the entity");
        _mockRepository.Verify(repo => repo.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ConcurrencyConflict_ThrowsException()
    {
        var posudekId = Guid.NewGuid();
        var posudek = CreateMockPosudek("v1", "PLATNY");
        var command = new InvalidatePosudekCommand(posudekId, "\"v2\"", new PosudekZneplatnitDto(), "corr-123");

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(posudekId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(posudek);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("ConcurrencyConflict", "because the IfMatch ETag does not match the current database version");
    }

    [Fact]
    public async Task Handle_StateConflict_ThrowsException()
    {
        var posudekId = Guid.NewGuid();
        var posudek = CreateMockPosudek("v1", "ZNEPLATNENY");
        var command = new InvalidatePosudekCommand(posudekId, "\"v1\"", new PosudekZneplatnitDto(), "corr-123");

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(posudekId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(posudek);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("StateConflict", "because the entity is already in the ZNEPLATNENY state");
    }

    [Fact]
    public async Task Handle_ValidRequest_InvalidatesPosudek_CommitsTransaction_ReturnsDto()
    {
        var posudekId = Guid.NewGuid();
        var posudek = CreateMockPosudek("v1", "PLATNY");
        var command = new InvalidatePosudekCommand(posudekId, "\"v1\"", new PosudekZneplatnitDto(), "corr-123");
        var mappedDto = new PosudekRoDetailDto { Id = posudekId };

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(posudekId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(posudek);

        _mockMapper
            .Setup(m => m.Map<PosudekRoDetailDto>(It.IsAny<PosudekRo>()))
            .Returns(mappedDto);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(posudekId);

        _mockRepository.Verify(repo => repo.Update(It.IsAny<PosudekRo>()), Times.Once);
        _mockRepository.Verify(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockTransaction.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockTransaction.Verify(t => t.RollbackAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_RollsBackTransaction_ThrowsException()
    {
        var posudekId = Guid.NewGuid();
        var posudek = CreateMockPosudek("v1", "PLATNY");
        var command = new InvalidatePosudekCommand(posudekId, "\"v1\"", new PosudekZneplatnitDto(), "corr-123");

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(posudekId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(posudek);

        _mockRepository
            .Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database connection lost"));

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>().WithMessage("Database connection lost");

        _mockTransaction.Verify(t => t.RollbackAsync(It.IsAny<CancellationToken>()), Times.Once);
        _mockTransaction.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
    private PosudekRo CreateMockPosudek(string verzeZaznamu, string stavPosudkuKod)
    {
        var pacient = new PacientVO("123", null, null, DateTime.MinValue, null, null, null, null);
        var pracovnik = new ZdravotnickyPracovnikVO("krzp", null, null, null, null, null);
        var poskytovatel = new PoskytovatelVO(null, null, null);
        var emptyRef = new CiselnikPolozkaReference("", "", "", new Dictionary<string, TranslationVO>());

        var stavRef = new CiselnikPolozkaReference("STAV", "1", stavPosudkuKod, new Dictionary<string, TranslationVO>());

        var hlavicka = new PosudekHlavicka(
            pacient, pracovnik, poskytovatel, emptyRef, stavRef, emptyRef, emptyRef, DateTime.UtcNow, null);

        var prop = typeof(PosudekHlavicka).GetProperty(nameof(PosudekHlavicka.VerzeZaznamu));
        if (prop != null && prop.CanWrite)
        {
            prop.SetValue(hlavicka, verzeZaznamu);
        }
        else
        {
            var field = typeof(PosudekHlavicka).GetField($"<{nameof(PosudekHlavicka.VerzeZaznamu)}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null) field.SetValue(hlavicka, verzeZaznamu);
        }

        return new PosudekRo(hlavicka);
    }
}