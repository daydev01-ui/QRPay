// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : Shared.Infrastructure
// Iteración : Fase 3 — Construcción, Iteración 1
// Autor     : [Tesista]
// Fecha     : 2026
namespace QRPayments.Shared.Infrastructure.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Request: {Method} {Path}", context.Request.Method, context.Request.Path);
        await _next(context);
        _logger.LogInformation("Response: {StatusCode}", context.Response.StatusCode);
    }
}
