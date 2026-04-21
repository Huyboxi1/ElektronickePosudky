using AutoMapper;
using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Repositories;
using ElektronickePosudky.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ElektronickePosudky.Application.Features.Posudky.Commands;

public record InvalidatePosudekCommand(Guid Id, string IfMatch, PosudekZneplatnitDto Data, string? CorrelationId) : IRequest<PosudekRoDetailDto>;

public class InvalidatePosudekCommandHandler : IRequestHandler<InvalidatePosudekCommand, PosudekRoDetailDto>
{
    private readonly IPosudekRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<InvalidatePosudekCommandHandler> _logger;

    public InvalidatePosudekCommandHandler(
        IPosudekRepository repository,
        IMapper mapper,
        ILogger<InvalidatePosudekCommandHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PosudekRoDetailDto> Handle(InvalidatePosudekCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting to invalidate Posudek ID: {PosudekId}. CorrelationId: {CorrelationId}",
            request.Id, request.CorrelationId);

        var posudek = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (posudek == null)
        {
            _logger.LogWarning("Posudek ID: {PosudekId} not found for invalidation. CorrelationId: {CorrelationId}",
                request.Id, request.CorrelationId);
            return null!;
        }

        var currentETag = posudek.Hlavicka.VerzeZaznamu;
        if (request.IfMatch != currentETag)
        {
            _logger.LogWarning("Concurrency Conflict for Posudek ID: {PosudekId}. Current ETag: {CurrentETag}, Requested ETag: {RequestETag}. CorrelationId: {CorrelationId}",
                request.Id, currentETag, request.IfMatch, request.CorrelationId);

            throw new InvalidOperationException("ConcurrencyConflict");
        }

        if (posudek.Hlavicka.StavPosudku.PolozkaKod == "stav_posudku_3")
        {
            _logger.LogWarning("State Conflict for Posudek ID: {PosudekId}. Opinion is already invalidated. CorrelationId: {CorrelationId}",
                request.Id, request.CorrelationId);

            throw new InvalidOperationException("StateConflict");
        }

        await using var transaction = await _repository.BeginTransactionAsync(cancellationToken);
        try
        {
            var invalidatedState = new CiselnikPolozkaReference(
                "stav_posudku_3",
                "1.0.0",
                "stav-posudku",
                new Dictionary<string, TranslationVO>
                {
                    { "cs", new TranslationVO("zneplatněný", "Zneplatněný") }
                }
            );

            posudek.Zneplatnit(invalidatedState);

            _repository.Update(posudek);
            await _repository.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Successfully invalidated Posudek ID: {PosudekId}. CorrelationId: {CorrelationId}",
                request.Id, request.CorrelationId);

            var updatedPosudek = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<PosudekRoDetailDto>(updatedPosudek);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Failed to invalidate Posudek ID: {PosudekId}. Transaction rolled back. CorrelationId: {CorrelationId}",
                request.Id, request.CorrelationId);
            throw;
        }
    }
}