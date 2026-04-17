using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Repositories;
using MediatR;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ElektronickePosudky.Application.Features.Posudky.Queries;

public record SearchPosudkyQuery(SearchPosudkyDto Data, string? CorrelationId) : IRequest<PagedResult<PosudekRoDetailDto>>;

public class SearchPosudkyQueryHandler : IRequestHandler<SearchPosudkyQuery, PagedResult<PosudekRoDetailDto>>
{
    private readonly IPosudekRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<SearchPosudkyQueryHandler> _logger;

    public SearchPosudkyQueryHandler(
        IPosudekRepository repository,
        IMapper mapper,
        ILogger<SearchPosudkyQueryHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<PagedResult<PosudekRoDetailDto>> Handle(SearchPosudkyQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start searching for Posudek. Page: {Page}, Size: {Size}. CorrelationId: {CorrelationId}",
            request.Data.Page, request.Data.Size, request.CorrelationId);

        var (items, totalCount) = await _repository.SearchAsync(
            request.Data.Rid,
            request.Data.DatumOd,
            request.Data.DatumDo,
            request.Data.JenPlatne,
            request.Data.StavPosudku,
            request.Data.Fulltext,
            request.Data.Ico,
            request.Data.Page,
            request.Data.Size,
            request.Data.Sort,
            request.Data.Order,
            cancellationToken);

        var dtoList = _mapper.Map<List<PosudekRoDetailDto>>(items);

        var result = new PagedResult<PosudekRoDetailDto>
        {
            Page = dtoList,
            TotalCount = totalCount,
            PageNumber = request.Data.Page,
            PageSize = request.Data.Size,
            NextPage = (request.Data.Page * request.Data.Size) < totalCount ? request.Data.Page + 1 : 0
        };

        _logger.LogInformation("Search completed. Returning {Count}/{TotalCount} results. CorrelationId: {CorrelationId}",
            result.Page.Count, totalCount, request.CorrelationId);

        return result;
    }
}