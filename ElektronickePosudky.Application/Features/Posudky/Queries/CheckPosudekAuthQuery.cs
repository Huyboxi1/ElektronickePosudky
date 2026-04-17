using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ElektronickePosudky.Application.Features.Posudky.Queries;

public record CheckPosudekAuthQuery(PosudekAuthCheckDto Data, string? CorrelationId) : IRequest<PosudekAuthCheckResponseDto>;

public class CheckPosudekAuthQueryHandler : IRequestHandler<CheckPosudekAuthQuery, PosudekAuthCheckResponseDto>
{
    private readonly IPosudekRepository _repository;
    private readonly ILogger<CheckPosudekAuthQueryHandler> _logger;

    public CheckPosudekAuthQueryHandler(
        IPosudekRepository repository,
        ILogger<CheckPosudekAuthQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PosudekAuthCheckResponseDto> Handle(CheckPosudekAuthQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Checking authorization for KRZP: {KrzpId}, ICO: {Ico}. CorrelationId: {CorrelationId}",
            request.Data.KrzpId, request.Data.Ico, request.CorrelationId);

        try
        {
            // Verify authorization from database: check if doctor (KRZP) and provider (ICO) exist together
            bool isAuthorized = await _repository.VerifyAuthorizationAsync(request.Data.KrzpId, request.Data.Ico, cancellationToken);

            if (!isAuthorized)
            {
                _logger.LogWarning("Authorization failed: No matching records found for KRZP {KrzpId}, ICO: {Ico}. CorrelationId: {CorrelationId}",
                    request.Data.KrzpId, request.Data.Ico, request.CorrelationId);
            }
            else
            {
                _logger.LogInformation("Authorization successful for KRZP {KrzpId}, ICO: {Ico}. CorrelationId: {CorrelationId}",
                    request.Data.KrzpId, request.Data.Ico, request.CorrelationId);
            }

            return new PosudekAuthCheckResponseDto { Opravneni = isAuthorized };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Authorization check failed for KRZP: {KrzpId}, ICO: {Ico}. CorrelationId: {CorrelationId}",
                request.Data.KrzpId, request.Data.Ico, request.CorrelationId);
            return new PosudekAuthCheckResponseDto { Opravneni = false };
        }
    }
}