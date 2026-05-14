using LibrarySys.Application.Common.Errors;
using System.Net;

namespace LibrarySysApi.Middleware;


/// <summary>
/// Global Exception Middleware
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = ex switch
        {
            ArgumentException or ArgumentNullException =>
                (StatusCodes.Status400BadRequest, "Invalid request parameters."),
            UnauthorizedAccessException =>
                (StatusCodes.Status401Unauthorized, "Unauthorized access."),
            KeyNotFoundException =>
                (StatusCodes.Status404NotFound, "Resource not found."),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please try again.")
        };

        context.Response.StatusCode = statusCode;

        var response = new ErrorResponse
        {
            Message = message,
            StatusCode = statusCode
        };

        #region old_switchCase
        //switch (ex)
        //{
        //    case ArgumentException or ArgumentNullException:
        //        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        //        response.StatusCode = 400;
        //        response.Message = "Invalid request parameters.";
        //        break;

        //    case UnauthorizedAccessException:
        //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //        response.StatusCode = 401;
        //        response.Message = "Unauthorized access.";
        //        break;

        //    case KeyNotFoundException:
        //        context.Response.StatusCode = StatusCodes.Status404NotFound;
        //        response.StatusCode = 404;
        //        response.Message = "Resource not found.";
        //        break;

        //    default:
        //        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        //        break;
        //}
        #endregion


        await context.Response.WriteAsJsonAsync(response);

    }


}


/// <summary>
/// a simple extention for registering the global middleware in program.cs
/// </summary>
public static class ExceptionMiddlewareExtention
{
    public static IApplicationBuilder UseGlobalExtentionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}

