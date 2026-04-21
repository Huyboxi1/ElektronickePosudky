using AutoMapper;
using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Repositories;
using ElektronickePosudky.Domain.Entities.PosudekAggregate;
using ElektronickePosudky.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ElektronickePosudky.Application.Features.Posudky.Commands;

public record CreatePosudekCommand(PosudekRoCreateDto Data, string? CorrelationId) : IRequest<CreatePosudekResponseDto>;

public class CreatePosudekCommandHandler : IRequestHandler<CreatePosudekCommand, CreatePosudekResponseDto>
{
    private readonly IPosudekRepository _repository;
    private readonly ILogger<CreatePosudekCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreatePosudekCommandHandler(
        IPosudekRepository repository,
        ILogger<CreatePosudekCommandHandler> logger,
        IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<CreatePosudekResponseDto> Handle(CreatePosudekCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Data;

        _logger.LogInformation("Starting create Posudek pro RID: {Rid}, KRZP: {KrzpId}. CorrelationId: {CorrelationId}",
            dto.Rid, dto.KrzpId, request.CorrelationId);

        await using var transaction = await _repository.BeginTransactionAsync(cancellationToken);
        try
        {
            var pacient = new PacientVO(
                dto.Rid,
                null, null, DateTime.MinValue, null, null, null, null
            );

            var zdravotnickyPracovnik = new ZdravotnickyPracovnikVO(
                dto.KrzpId,
                null, null, null, null, null
            );

            var poskytovatel = new PoskytovatelVO(null, null, null);

            var odbornostLekare = new CiselnikPolozkaReference(
                string.Empty,
                string.Empty,
                string.Empty,
                new Dictionary<string, TranslationVO> { { "cs", new TranslationVO(string.Empty, string.Empty) } }
            );

            var hlavicka = new PosudekHlavicka(
            pacient,
            zdravotnickyPracovnik,
            poskytovatel,
            odbornostLekare,
            MapToReference("akce-ro", dto.TypAkceKod ?? "akce_ro_1"),
            MapToReference("stav-posudku", dto.StavPosudkuKod),
            MapToReference("druh-prohlidky-ro", dto.DruhProhlidkyKod),
            MapToReference("druh-posudku-ro", dto.DruhPosudkuKod),
            dto.DatumVystaveni,
            dto.PlatnostDo
        );

            var posudek = new PosudekRo(hlavicka);

            if (dto.Zpusobilosti != null && dto.Zpusobilosti.Count > 0)
            {
                foreach (var zpusobilostDto in dto.Zpusobilosti)
                {
                    var zpusobilost = new PosudekZpusobilost(
                        MapToReference("skupina-zadatel-ridic-ro", zpusobilostDto.SkupinaZadateleRidicKod),
                        MapToReference("vysledek-posudku-ro", zpusobilostDto.VysledekKod)
                    );

                    if (zpusobilostDto.SkupinyRidicskehoOpravneniKody != null)
                    {
                        foreach (var skupinaKod in zpusobilostDto.SkupinyRidicskehoOpravneniKody)
                        {
                            var skupina = new PosudekSkupinaRo(
                                MapToReference("seznam-skupin-ro", skupinaKod)
                            );
                            zpusobilost.AddSkupinaRidicskehoOpravneni(skupina);
                        }
                    }

                    if (zpusobilostDto.HarmonizovaneKody != null)
                    {
                        foreach (var harmKodDto in zpusobilostDto.HarmonizovaneKody)
                        {
                            var harmKod = new PosudekHarmonizovanyKod(
                                MapToReference("seznam-harmonizovane-kody-ro", harmKodDto.HarmonizovanyKod),
                                null,
                                harmKodDto.UpresneniText
                            );

                            if (harmKodDto.SkupinaRoKody != null)
                            {
                                foreach (var skupinaRoKod in harmKodDto.SkupinaRoKody)
                                {
                                    harmKod.AddSkupinaRo(MapToReference("seznam-skupin-ro", skupinaRoKod));
                                }
                            }

                            zpusobilost.AddHarmonizovanyKod(harmKod);
                        }
                    }

                    if (zpusobilostDto.NarodniKody != null)
                    {
                        foreach (var natKodDto in zpusobilostDto.NarodniKody)
                        {
                            var natKod = new PosudekNarodniKod(
                                MapToReference("seznam-narodni-kody-ro", natKodDto.NarodniKod),
                                MapToReference("seznam-skupin-ro", natKodDto.SkupinaRoKod),
                                natKodDto.UpresneniText
                            );
                            zpusobilost.AddNarodniKod(natKod);
                        }
                    }

                    posudek.AddZpusobilost(zpusobilost);
                }

                _logger.LogInformation("Added {ZpusobilostCount} zpusobilosti to Posudek. CorrelationId: {CorrelationId}",
                    dto.Zpusobilosti.Count, request.CorrelationId);
            }

            // Save to database
            await _repository.AddAsync(posudek, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Create Success Posudek ID: {PosudekId} for RID: {Rid}, KRZP: {KrzpId}. CorrelationId: {CorrelationId}",
                posudek.Id, dto.Rid, dto.KrzpId, request.CorrelationId);

            var response = new CreatePosudekResponseDto
            {
                Id = posudek.Id,
                Hlavicka = _mapper.Map<PosudekHlavickaResponseDto>(posudek.Hlavicka),
                Zpusobilosti = _mapper.Map<List<PosudekZpusobilostResponseDto>>(posudek.Zpusobilosti)
                    .Select(z => { z.PosudekId = posudek.Id; return z; }).ToList()
            };

            return response;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Failed to create Posudek for RID: {Rid}, KRZP: {KrzpId}. Transaction rolled back. CorrelationId: {CorrelationId}",
                dto.Rid, dto.KrzpId, request.CorrelationId);
            throw;
        }
    }

    private CiselnikPolozkaReference MapToReference(string ciselnikKod, string polozkaKod)
    {
        return new CiselnikPolozkaReference(
            ciselnikKod,
            "1.0.0",
            polozkaKod,
            new Dictionary<string, TranslationVO> { { "cs", new TranslationVO(polozkaKod, "") } }
        );
    }
}