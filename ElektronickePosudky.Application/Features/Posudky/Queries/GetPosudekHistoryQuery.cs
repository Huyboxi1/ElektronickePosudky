using AutoMapper;
using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ElektronickePosudky.Application.Features.Posudky.Queries;

public record GetPosudekHistoryQuery(Guid Id, string? CorrelationId) : IRequest<List<PosudekHistorieDto>?>;

public class GetPosudekHistoryQueryHandler : IRequestHandler<GetPosudekHistoryQuery, List<PosudekHistorieDto>?>
{
    private readonly IPosudekRepository _repository;
    private readonly ILogger<GetPosudekHistoryQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetPosudekHistoryQueryHandler(
        IPosudekRepository repository,
        ILogger<GetPosudekHistoryQueryHandler> logger,
        IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<PosudekHistorieDto>?> Handle(GetPosudekHistoryQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start get histories for Posudek ID: {PosudekId}. CorrelationId: {CorrelationId}",
            request.Id, request.CorrelationId);

        var posudek = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (posudek == null)
        {
            _logger.LogWarning("Notfound Posudek ID: {PosudekId} to get histories. CorrelationId: {CorrelationId}",
                request.Id, request.CorrelationId);
            return null;
        }

        // Map domain history records to DTOs
        var result = _mapper.Map<List<PosudekHistorieDto>>(posudek.Historie.ToList());

        // If no history records exist, create a default creation record
        if (result.Count == 0)
        {
            var creationHistory = new PosudekHistorieDto
            {
                TypOperace = new CiselnikPolozkaReferenceDto
                {
                    CiselnikKod = "VYTVORENI",
                    CiselnikVerze = "1.0",
                    PolozkaKod = "VYTVORENI",
                    Preklady = new Dictionary<string, TranslationItemDto>
                    {
                        { "cs", new TranslationItemDto { Nazev = "Vytvoření", Popis = "Vytvoření posudku" } }
                    }
                },
                DatumOperace = posudek.Hlavicka.DatumVytvoreni,
                Lekar = _mapper.Map<ZdravotnickyPracovnikDetailDto>(posudek.Hlavicka.ZdravotnickyPracovnik),
                Poskytovatel = _mapper.Map<PoskytovatelDetailDto>(posudek.Hlavicka.PoskytovatelZdravotnickychSluzeb)
            };
            result = new List<PosudekHistorieDto> { creationHistory };
        }

        _logger.LogInformation("Successfully retrieved histories. Returning {Count} records for Posudek ID: {PosudekId}. CorrelationId: {CorrelationId}",
            result.Count, request.Id, request.CorrelationId);

        return result;
    }
}