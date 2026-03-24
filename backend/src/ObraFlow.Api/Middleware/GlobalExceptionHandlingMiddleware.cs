using System.Text.Json;

namespace ObraFlow.Api.Middleware;

public sealed class GlobalExceptionHandlingMiddleware
{
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(
                $"Unhandled exception. TraceId={context.TraceIdentifier}. {exception}");

            if (context.Response.HasStarted)
            {
                throw;
            }

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var payload = JsonSerializer.Serialize(new
            {
                message = "An unexpected error occurred.",
                traceId = context.TraceIdentifier
            }, SerializerOptions);

            await context.Response.WriteAsync(payload);
        }
    }
}
