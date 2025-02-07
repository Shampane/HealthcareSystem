using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareSystem.Api.Middlewares;

public class AppExceptionHandler : IExceptionHandler {
    private readonly ILogger<AppExceptionHandler> _logger;

    public AppExceptionHandler(ILogger<AppExceptionHandler> logger) {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken ct
    ) {
        _logger.LogError(
            exception, "Unhandled exception: {Message}", exception.Message
        );
        ProblemDetails problemsDetails = new() {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server Error"
        };
        httpContext.Response.StatusCode = problemsDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemsDetails, ct);
        return true;
    }
}