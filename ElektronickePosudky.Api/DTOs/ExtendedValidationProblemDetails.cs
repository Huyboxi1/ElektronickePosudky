using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace ElektronickePosudky.Api.DTOs;

public class ExtendedValidationProblemDetails : ValidationProblemDetails
{
    [JsonPropertyName("correlationId")]
    public string? CorrelationId { get; set; }

    public ExtendedValidationProblemDetails() : base()
    {
    }

    public ExtendedValidationProblemDetails(IDictionary<string, string[]> errors) : base(errors)
    {
    }
}
