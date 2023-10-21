using ApiCorrelation.Configurations.Interfaces;

namespace ApiCorrelation.Helpers;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private const string _correlationIdHeader = "X-Correlation-Id";

    public async Task Invoke(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
    {
        var correlationId = GetCorrelationTrace(context, correlationIdGenerator);

        AddCorrelationIdToResponse(context, correlationId);

        await _next(context);
    }

    private static void AddCorrelationIdToResponse(HttpContext context, string correlationId)
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers.Add(_correlationIdHeader, new[] { correlationId });
            return Task.CompletedTask;
        });
    }

    private static string GetCorrelationTrace(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
    {
        if (context.Request.Headers.TryGetValue(_correlationIdHeader, out var correlationId))
        {
            correlationIdGenerator.Set(correlationId);
            return correlationId;
        }
        else
        {
            return correlationIdGenerator.Get();
        }
    }
}
