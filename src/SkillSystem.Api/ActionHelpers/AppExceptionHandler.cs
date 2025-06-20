using Microsoft.AspNetCore.Diagnostics;
using SkillSystem.Domain.Errors;
using System.Net;
using System.Text.Json;

namespace SkillSystem.Api.ActionHelpers;

public class AppExceptionHandler : IExceptionHandler
{
    private readonly ILogger<AppExceptionHandler> _logger;

    public AppExceptionHandler(ILogger<AppExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError("Exception: {Message}", exception.Message);

        int statusCode = exception switch
        {
            ForbiddenException => 403,
            InvalidArgumentException => 422,
            NotFoundException or DirectoryNotFoundException or EntityNotFoundException => 404,
            AuthException or UnauthorizedException => 401,
            NotAllowedException => 403,
            DuplicateEntryException => 409,
            ValidationFailedException => 400,
            _ => (int)HttpStatusCode.InternalServerError
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        var response = new { error = exception.Message };
        var json = JsonSerializer.Serialize(response);

        await httpContext.Response.WriteAsync(json, cancellationToken);

        return true;
    }
}
