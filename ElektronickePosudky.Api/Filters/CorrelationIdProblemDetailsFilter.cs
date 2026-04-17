using ElektronickePosudky.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ElektronickePosudky.Api.Filters;

public class CorrelationIdProblemDetailsFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult && objectResult.Value is ProblemDetails problemDetails)
        {
            problemDetails.Extensions.Remove("traceId");

            if (context.HttpContext.Request.Headers.TryGetValue("X-Correlation-Id", out var correlationId))
            {
                if (problemDetails is ExtendedValidationProblemDetails extended)
                {
                    if (string.IsNullOrEmpty(extended.CorrelationId))
                    {
                        extended.CorrelationId = correlationId.ToString();
                    }
                }
                else if (!problemDetails.Extensions.ContainsKey("correlationId"))
                {
                    problemDetails.Extensions["correlationId"] = correlationId.ToString();
                }
            }
        }
    }
}
