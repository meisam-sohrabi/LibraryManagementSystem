using System.Net;

namespace LibrarySysApi.Middleware;


/// <summary>
/// Global Exceptoin Middleware
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);

        }catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await HandleExceptionAsync(context,ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context,Exception ex)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = 500;
            //(int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync("An unexpected error occurred. Please try again.");
    }
}

