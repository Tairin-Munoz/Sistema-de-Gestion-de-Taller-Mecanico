using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using TallerMecanico.Api.Responses;

namespace TallerMecanico.Api;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
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

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        object? errors = null;
        var message = exception.Message;

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                errors = validationException.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());
                break;
            case KeyNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                break;
            case UnauthorizedAccessException:
            case System.Security.Authentication.AuthenticationException:
                statusCode = HttpStatusCode.Unauthorized;
                break;
            case ArgumentException:
                statusCode = HttpStatusCode.BadRequest;
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var apiResponse = new ApiResponse<object>(null, false, message)
        {
            Errors = errors
        };

        await context.Response.WriteAsJsonAsync(apiResponse);
    }
}
