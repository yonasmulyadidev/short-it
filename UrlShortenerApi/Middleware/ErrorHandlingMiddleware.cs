using System.Net;
using Application.Exceptions;
using Newtonsoft.Json;

namespace UrlShortenerApi.Middleware;

public sealed class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        context.Response.StatusCode = exception switch
        {
            ValidationErrorException => (int)HttpStatusCode.BadRequest,
            NotFoundException => (int) HttpStatusCode.NotFound,
            DuplicateException => (int) HttpStatusCode.Conflict,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var errorMessage = ConstructExceptionMessage(exception);
        
        return context.Response.WriteAsync(errorMessage);
    }

    private static string ConstructExceptionMessage(Exception exception)
    {
        if (exception is ValidationErrorException errorException)
        {
            return JsonConvert.SerializeObject(new
            {
                error = errorException.Errors
            });
        }

        return JsonConvert.SerializeObject(new
        {
            error = exception.Message
        });
    }
}