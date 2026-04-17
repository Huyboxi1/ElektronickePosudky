using ElektronickePosudky.Application.Interfaces;
using ElektronickePosudky.Application.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ElektronickePosudky.Application.Features.Posudky.Queries;

public record GetPosudekPdfQuery(Guid Id, string? CorrelationId) : IRequest<byte[]?>;

public class GetPosudekPdfQueryHandler : IRequestHandler<GetPosudekPdfQuery, byte[]?>
{
    private readonly IPosudekRepository _repository;
    private readonly IPdfExportService _pdfService;
    private readonly ILogger<GetPosudekPdfQueryHandler> _logger;

    public GetPosudekPdfQueryHandler(
        IPosudekRepository repository,
        IPdfExportService pdfService,
        ILogger<GetPosudekPdfQueryHandler> logger)
    {
        _repository = repository;
        _pdfService = pdfService;
        _logger = logger;
    }

    public async Task<byte[]?> Handle(GetPosudekPdfQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting PDF export for Posudek ID: {PosudekId}. CorrelationId: {CorrelationId}",
            request.Id, request.CorrelationId);

        var posudek = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (posudek == null)
        {
            _logger.LogWarning("Not found Posudek ID: {PosudekId} to export PDF.", request.Id);
            return null;
        }

        var pdfBytes = _pdfService.GeneratePosudekPdf(posudek);

        _logger.LogInformation("Export PDF file successfully: {Size} bytes.", pdfBytes.Length);

        return pdfBytes;
    }
}