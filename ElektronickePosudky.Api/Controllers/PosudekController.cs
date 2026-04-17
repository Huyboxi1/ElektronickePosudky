using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Posudky.Commands;
using ElektronickePosudky.Api.Resources;
using ElektronickePosudky.Api.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ElektronickePosudky.Application.Features.Posudky.Queries;

namespace ElektronickePosudky.Api.Controllers;

[ApiController]
[Route("api/v2/posudky/ridicskeOpravneni")]
public class PosudekController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public PosudekController(IMediator mediator, IStringLocalizer<SharedResource> localizer)
    {
        _mediator = mediator;
        _localizer = localizer;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreatePosudekResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status408RequestTimeout)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create(
            [FromHeader(Name = "X-Correlation-Id")] string? correlationId,
            [FromHeader(Name = "Accept-Language")] string? acceptLanguage,
            [FromBody] PosudekRoCreateDto request,
            CancellationToken cancellationToken)
    {
        var command = new CreatePosudekCommand(request, correlationId);

        var response = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = response.Id },
            value: response
        );
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PosudekRoDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        [FromHeader(Name = "X-Correlation-Id")] string? correlationId,
        [FromHeader(Name = "Accept-Language")] string? acceptLanguage,
        [FromHeader(Name = "If-None-Match")] string? ifNoneMatch,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPosudekByIdQuery(id, correlationId), cancellationToken);

        if (result == null)
        {
            return NotFound(new ExtendedValidationProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = _localizer["PosudekNotFoundTitle"],
                Detail = string.Format(_localizer["PosudekNotFoundDetail"], id)
            });
        }

        var currentETag = $"\"{result.VerzeZaznamu}\"";

        if (!string.IsNullOrEmpty(ifNoneMatch) && ifNoneMatch == currentETag)
        {
            Response.Headers.Append("ETag", currentETag);
            return StatusCode(StatusCodes.Status304NotModified);
        }

        Response.Headers.Append("ETag", currentETag);
        return Ok(result);
    }

    [HttpPost("vyhledat")]
    [ProducesResponseType(typeof(PagedResult<PosudekRoDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status408RequestTimeout)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Search(
            [FromHeader(Name = "X-Correlation-Id")] string? correlationId,
            [FromHeader(Name = "Accept-Language")] string? acceptLanguage,
            [FromBody] SearchPosudkyDto request,
            CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SearchPosudkyQuery(request, correlationId), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id}/historie")]
    [ProducesResponseType(typeof(IEnumerable<PosudekHistorieDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHistory(
        [FromRoute] Guid id,
        [FromHeader(Name = "X-Correlation-Id")] string? correlationId,
        [FromHeader(Name = "Accept-Language")] string? acceptLanguage,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPosudekHistoryQuery(id, correlationId), cancellationToken);

        if (result == null)
        {
            return NotFound(new ExtendedValidationProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = _localizer["PosudekNotFoundTitle"],
                Detail = string.Format(_localizer["PosudekNotFoundDetail"], id),
            });
        }

        return Ok(result);
    }

    [HttpPatch("{id}/zneplatnit")]
    [ProducesResponseType(typeof(PosudekRoDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status408RequestTimeout)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status412PreconditionFailed)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Invalidate(
        [FromRoute] Guid id,
        [FromHeader(Name = "If-Match")] string ifMatch,
        [FromHeader(Name = "X-Correlation-Id")] string? correlationId,
        [FromHeader(Name = "Accept-Language")] string? acceptLanguage,
        [FromBody] PosudekZneplatnitDto request,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new InvalidatePosudekCommand(id, ifMatch, request, correlationId);

            var result = await _mediator.Send(command, cancellationToken);

            if (result == null)
            {
                return NotFound(new ExtendedValidationProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = _localizer["PosudekNotFoundTitle"],
                    Detail = string.Format(_localizer["PosudekNotFoundDetail"], id)
                });
            }

            var currentETag = $"\"{result.VerzeZaznamu}\"";
            Response.Headers.Append("ETag", currentETag);

            return Ok(result);
        }
        catch (InvalidOperationException ex) when (ex.Message == "ConcurrencyConflict")
        {
            return StatusCode(StatusCodes.Status412PreconditionFailed, new ExtendedValidationProblemDetails
            {
                Status = StatusCodes.Status412PreconditionFailed,
                Title = _localizer["ConcurrencyConflictTitle"],
                Detail = _localizer["ConcurrencyConflictDetail"]
            });
        }
        catch (InvalidOperationException ex) when (ex.Message == "StateConflict")
        {
            return StatusCode(StatusCodes.Status409Conflict, new ExtendedValidationProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = _localizer["StateConflictTitle"],
                Detail = _localizer["StateConflictDetail"]
            });
        }
    }

    [HttpGet("{id}/pdf")]
    [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK, "application/pdf")]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status408RequestTimeout)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPdf(
        [FromRoute] Guid id,
        [FromHeader(Name = "X-Correlation-Id")] string? correlationId,
        [FromHeader(Name = "Accept-Language")] string? acceptLanguage,
        CancellationToken cancellationToken)
    {
        var pdfBytes = await _mediator.Send(new GetPosudekPdfQuery(id, correlationId), cancellationToken);

        if (pdfBytes == null || pdfBytes.Length == 0)
        {
            return NotFound(new ExtendedValidationProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = _localizer["PosudekNotFoundTitle"],
                Detail = string.Format(_localizer["PosudekNotFoundDetail"], id)
            });
        }

        return File(pdfBytes, "application/pdf", $"posudek_{id}.pdf");
    }

    [HttpPost("zalozeni/opravneni")]
    [ProducesResponseType(typeof(PosudekAuthCheckResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status408RequestTimeout)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CheckAuthorization(
        [FromHeader(Name = "X-Correlation-Id")] string? correlationId,
        [FromHeader(Name = "Accept-Language")] string? acceptLanguage,
        [FromBody] PosudekAuthCheckDto request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CheckPosudekAuthQuery(request, correlationId), cancellationToken);

        return Ok(response);
    }
}