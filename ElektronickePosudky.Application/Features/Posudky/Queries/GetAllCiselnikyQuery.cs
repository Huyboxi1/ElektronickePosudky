using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ElektronickePosudky.Application.Features.Ciselniky.Queries;

public record GetAllCiselnikyQuery(string? correlationId) : IRequest<List<CiselnikDto>>;

public class GetAllCiselnikyQueryHandler : IRequestHandler<GetAllCiselnikyQuery, List<CiselnikDto>>
{
    private readonly ICiselnikRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllCiselnikyQueryHandler> _logger;

    public GetAllCiselnikyQueryHandler(ICiselnikRepository repository, IMapper mapper, ILogger<GetAllCiselnikyQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<CiselnikDto>> Handle(GetAllCiselnikyQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching all codebooks. CorrelationId: {CorrelationId}", request.correlationId);

        var ciselniky = await _repository.GetAllAsync(cancellationToken);
        var result = _mapper.Map<List<CiselnikDto>>(ciselniky);

        _logger.LogInformation("Successfully fetched {Count} codebooks. CorrelationId: {CorrelationId}", result.Count, request.correlationId);
        return result;
    }
}