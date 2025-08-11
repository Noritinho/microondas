using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Microwave.Infrastructure;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            LogException(ex);

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var result = System.Text.Json.JsonSerializer.Serialize(new
            {
                error = "An unexpected error occurred."
            });

            await httpContext.Response.WriteAsync(result);
        }
    }

    private void LogException(Exception ex)
    {
        var logFolder = Path.Combine(AppContext.BaseDirectory, "logs");
        if (!Directory.Exists(logFolder))
            Directory.CreateDirectory(logFolder);

        var logFilePath = Path.Combine(logFolder, "errors.log");

        var logMessage = $@"
[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff}] Exception: {ex.Message}
Inner Exception: {ex.InnerException?.Message ?? "None"}
Stack Trace:
{ex.StackTrace}
-----------------------------------------------------
";

        File.AppendAllText(logFilePath, logMessage);
    }
}