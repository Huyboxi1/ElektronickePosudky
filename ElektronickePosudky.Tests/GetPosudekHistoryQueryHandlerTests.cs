using AutoMapper;
using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Queries;
using ElektronickePosudky.Application.Repositories;
using ElektronickePosudky.Domain.Entities.PosudekAggregate;
using ElektronickePosudky.Domain.ValueObjects;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ElektronickePosudky.Tests;

public class GetPosudekHistoryQueryHandlerTests
{
    private readonly Mock<IPosudekRepository> _mockRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<GetPosudekHistoryQueryHandler>> _mockLogger;
    private readonly GetPosudekHistoryQueryHandler _handler;

    public GetPosudekHistoryQueryHandlerTests()
    {
        _mockRepository = new Mock<IPosudekRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<GetPosudekHistoryQueryHandler>>();

        _handler = new GetPosudekHistoryQueryHandler(
            _mockRepository.Object,
            _mockLogger.Object,
            _mockMapper.Object
        );
    }

    [Fact]
    public async Task Handle_PosudekNotFound_ReturnsNull()
    {
        var query = new GetPosudekHistoryQuery(Guid.NewGuid(), "corr-123");

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((PosudekRo)null!);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeNull("because the Posudek does not exist in the database");
        _mockMapper.Verify(m => m.Map<List<PosudekHistorieDto>>(It.IsAny<object>()), Times.Never);
    }

    [Fact]
    public async Task Handle_PosudekHasExistingHistory_ReturnsMappedHistory()
    {
        var query = new GetPosudekHistoryQuery(Guid.NewGuid(), "corr-123");
        var posudek = CreateMockPosudek();

        var existingHistory = new List<PosudekHistorieDto>
        {
            new PosudekHistorieDto { DatumOperace = DateTime.UtcNow.AddDays(-1) },
            new PosudekHistorieDto { DatumOperace = DateTime.UtcNow }
        };

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(posudek);

        _mockMapper
            .Setup(m => m.Map<List<PosudekHistorieDto>>(It.IsAny<IReadOnlyCollection<PosudekHistorie>>()))
            .Returns(existingHistory);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().HaveCount(2, "because it should return the existing history records");
    }

    [Fact]
    public async Task Handle_PosudekHasNoHistory_CreatesAndReturnsDefaultCreationHistory()
    {
        var query = new GetPosudekHistoryQuery(Guid.NewGuid(), "corr-123");
        var posudek = CreateMockPosudek();

        var emptyHistory = new List<PosudekHistorieDto>();

        var mockLekarDto = new ZdravotnickyPracovnikDetailDto { KrzpId = "krzp-123" };
        var mockPoskytovatelDto = new PoskytovatelDetailDto { Ico = "ico-456" };

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(posudek);

        _mockMapper
            .Setup(m => m.Map<List<PosudekHistorieDto>>(It.IsAny<IReadOnlyCollection<PosudekHistorie>>()))
            .Returns(emptyHistory);

        _mockMapper
            .Setup(m => m.Map<ZdravotnickyPracovnikDetailDto>(posudek.Hlavicka.ZdravotnickyPracovnik))
            .Returns(mockLekarDto);
        _mockMapper
            .Setup(m => m.Map<PoskytovatelDetailDto>(posudek.Hlavicka.PoskytovatelZdravotnickychSluzeb))
            .Returns(mockPoskytovatelDto);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().HaveCount(1);

        var creationRecord = result!.First();

        creationRecord.TypOperace.PolozkaKod.Should().Be("akce_ro_1");

        creationRecord.Lekar.Should().BeEquivalentTo(mockLekarDto);
        creationRecord.Poskytovatel.Should().BeEquivalentTo(mockPoskytovatelDto);
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_ThrowsException()
    {
        var query = new GetPosudekHistoryQuery(Guid.NewGuid(), "corr-123");

        _mockRepository
            .Setup(repo => repo.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database timeout"));

        Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>().WithMessage("Database timeout");
    }

    private PosudekRo CreateMockPosudek()
    {
        var pacient = new PacientVO("1234567890", null, null, DateTime.MinValue, null, null, null, null);
        var pracovnik = new ZdravotnickyPracovnikVO("krzp-123", null, null, null, null, null);
        var poskytovatel = new PoskytovatelVO("12345678", "Nemocnice", null);

        var emptyRef = new CiselnikPolozkaReference("kod", "1.0.0", "val", new Dictionary<string, TranslationVO>());

        var typAkceRef = new CiselnikPolozkaReference("akce-ro", "1.0.0", "akce_ro_1", new Dictionary<string, TranslationVO>());

        var hlavicka = new PosudekHlavicka(
            pacient,
            pracovnik,
            poskytovatel,
            emptyRef,
            typAkceRef,
            emptyRef,
            emptyRef,
            emptyRef,
            DateTime.UtcNow,
            null);

        return new PosudekRo(hlavicka);
    }
}