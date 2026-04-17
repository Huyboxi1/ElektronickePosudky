using ElektronickePosudky.Api.Resources;
using ElektronickePosudky.Api.DTOs;
using ElektronickePosudky.Application.DTOs;
using ElektronickePosudky.Application.Features.Ciselniky.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ElektronickePosudky.Api.Controllers;

[ApiController]
[Route("api/v2/ciselniky")]
public class CiselnikController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IStringLocalizer<SharedResource> _localizer;

    public CiselnikController(IMediator mediator, IStringLocalizer<SharedResource> localizer)
    {
        _mediator = mediator;
        _localizer = localizer;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CiselnikDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status408RequestTimeout)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(
        [FromHeader(Name = "X-Correlation-Id")] string? correlationId,
        [FromHeader(Name = "Accept-Language")] string? acceptLanguage,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetAllCiselnikyQuery(correlationId), cancellationToken);
            return Ok(result);
        }
        catch (OperationCanceledException)
        {
            return StatusCode(StatusCodes.Status408RequestTimeout, new ExtendedValidationProblemDetails
            {
                Status = StatusCodes.Status408RequestTimeout,
                Title = _localizer["RequestTimeoutTitle"],
                Detail = _localizer["RequestTimeoutDetail"]
            });
        }
    }

    [HttpGet("{kod}/polozky")]
    [ProducesResponseType(typeof(List<CiselnikPolozkaDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExtendedValidationProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetItems(
        [FromRoute] string kod,
        [FromHeader(Name = "X-Correlation-Id")] string? correlationId,
        [FromHeader(Name = "Accept-Language")] string? acceptLanguage,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetCiselnikItemsQuery(kod, correlationId), cancellationToken);

        if (result == null || result.Count == 0)
        {
            return NotFound(new ExtendedValidationProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = _localizer["CiselnikNotFoundTitle"],
                Detail = string.Format(_localizer["CiselnikNotFoundDetail"], kod)
            });
        }

        return Ok(result);
    }
}