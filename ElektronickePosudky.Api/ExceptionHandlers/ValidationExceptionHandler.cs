using ElektronickePosudky.Api.DTOs;
using ElektronickePosudky.Api.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace ElektronickePosudky.Api.ExceptionHandlers;

public class ValidationExceptionHandler : IExceptionHandler
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<ValidationExceptionHandler> _logger;

    public ValidationExceptionHandler(
        IStringLocalizer<SharedResource> localizer,
        IProblemDetailsService problemDetailsService,
        ILogger<ValidationExceptionHandler> logger)
    {
        _localizer = localizer;
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not FluentValidation.ValidationException validationException)
        {
            _logger.LogDebug("Exception is not a ValidationException. Type: {Type}", exception.GetType().Name);
            return false;
        }

        _logger.LogWarning("Processing validation exception. Errors: {ErrorCount}", validationException.Errors.Count());

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        var errors = validationException.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x =>
                {
                    var localized = _localizer[x.ErrorMessage];
                    _logger.LogDebug("Localizing error: Key={Key}, Value={Value}, ResourceNotFound={NotFound}", x.ErrorMessage, localized.Value, localized.ResourceNotFound);
                    return localized.Value ?? x.ErrorMessage;
                })
                    .Distinct()
                    .ToArray()
            );

        var problemDetails = new ExtendedValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = _localizer["ValidationErrorTitle"],
            Instance = httpContext.Request.Path
        };

        if (httpContext.Request.Headers.TryGetValue("X-Correlation-Id", out var correlationId))
        {
            problemDetails.CorrelationId = correlationId.ToString();
        }

        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}