using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ElektronickePosudky.Application.Features.Ciselniky.Queries;

public record GetCiselnikItemsQuery(string Kod, string? correlationId = null) : IRequest<List<CiselnikPolozkaDto>>;

public class GetCiselnikItemsQueryHandler : IRequestHandler<GetCiselnikItemsQuery, List<CiselnikPolozkaDto>>
{
    private readonly ICiselnikRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCiselnikItemsQueryHandler> _logger;

    public GetCiselnikItemsQueryHandler(ICiselnikRepository repository, IMapper mapper, ILogger<GetCiselnikItemsQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<CiselnikPolozkaDto>> Handle(GetCiselnikItemsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching codebook items for code: {Code}. CorrelationId: {CorrelationId}", request.Kod, request.correlationId);

        var items = await _repository.GetItemsByCodeAsync(request.Kod, cancellationToken);
        var result = _mapper.Map<List<CiselnikPolozkaDto>>(items);

        _logger.LogInformation("Successfully fetched {Count} items for codebook: {Code}. CorrelationId: {CorrelationId}", result.Count, request.Kod, request.correlationId);
        return result;
    }
}