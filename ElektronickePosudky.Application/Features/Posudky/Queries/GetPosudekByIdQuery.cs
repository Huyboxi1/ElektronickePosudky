using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Repositories;
using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ElektronickePosudky.Application.Features.Posudky.Queries;

public record GetPosudekByIdQuery(Guid Id, string? CorrelationId) : IRequest<PosudekRoDetailDto?>;

public class GetPosudekByIdQueryHandler : IRequestHandler<GetPosudekByIdQuery, PosudekRoDetailDto?>
{
    private readonly IPosudekRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetPosudekByIdQueryHandler> _logger;

    public GetPosudekByIdQueryHandler(
        IPosudekRepository repository,
        IMapper mapper,
        ILogger<GetPosudekByIdQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PosudekRoDetailDto?> Handle(GetPosudekByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start query Posudek ID: {PosudekId}. CorrelationId: {CorrelationId}",
            request.Id, request.CorrelationId);

        var posudek = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (posudek == null)
        {
            _logger.LogWarning("Not found Posudek ID: {PosudekId}. CorrelationId: {CorrelationId}",
                request.Id, request.CorrelationId);
            return null;
        }

        var result = _mapper.Map<PosudekRoDetailDto>(posudek);

        _logger.LogInformation("Query successful for Posudek ID: {PosudekId}. CorrelationId: {CorrelationId}",
            request.Id, request.CorrelationId);

        return result;
    }
}