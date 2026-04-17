using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Queries;
using ElektronickePosudky.Application.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ElektronickePosudky.Tests;

public class CheckPosudekAuthQueryHandlerTests
{
    private readonly Mock<IPosudekRepository> _mockRepository;
    private readonly Mock<ILogger<CheckPosudekAuthQueryHandler>> _mockLogger;
    private readonly CheckPosudekAuthQueryHandler _handler;

    public CheckPosudekAuthQueryHandlerTests()
    {
        _mockRepository = new Mock<IPosudekRepository>();
        _mockLogger = new Mock<ILogger<CheckPosudekAuthQueryHandler>>();

        _handler = new CheckPosudekAuthQueryHandler(
            _mockRepository.Object,
            _mockLogger.Object
        );
    }

    [Fact]
    public async Task Handle_AuthorizedUser_ReturnsOpravneniTrue()
    {
        var dummyData = new PosudekAuthCheckDto
        {
            KrzpId = "KRZP-123",
            Ico = "ICO-456"
        };
        var query = new CheckPosudekAuthQuery(dummyData, "corr-123");

        _mockRepository
            .Setup(repo => repo.VerifyAuthorizationAsync(dummyData.KrzpId, dummyData.Ico, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Opravneni.Should().BeTrue("because the repository confirmed the user is authorized");

        _mockRepository.Verify(
            repo => repo.VerifyAuthorizationAsync("KRZP-123", "ICO-456", It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_UnauthorizedUser_ReturnsOpravneniFalse()
    {
        // 1. ARRANGE
        var dummyData = new PosudekAuthCheckDto
        {
            KrzpId = "KRZP-999",
            Ico = "ICO-999"
        };
        var query = new CheckPosudekAuthQuery(dummyData, "corr-123");

        _mockRepository
            .Setup(repo => repo.VerifyAuthorizationAsync(dummyData.KrzpId, dummyData.Ico, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Opravneni.Should().BeFalse("because the repository could not find a matching record");

        _mockRepository.Verify(
            repo => repo.VerifyAuthorizationAsync("KRZP-999", "ICO-999", It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_ReturnsOpravneniFalse()
    {
        var dummyData = new PosudekAuthCheckDto
        {
            KrzpId = "KRZP-ERROR",
            Ico = "ICO-ERROR"
        };
        var query = new CheckPosudekAuthQuery(dummyData, "corr-123");

        _mockRepository
            .Setup(repo => repo.VerifyAuthorizationAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database timeout"));

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Opravneni.Should().BeFalse("because an exception occurred and the handler should default to unauthorized safely");
    }
}