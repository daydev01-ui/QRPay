// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : MockBCP.PaymentService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-00] → [CU-00] → [PaymentController] → [IntegrationTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.AspNetCore.Mvc;

namespace MockBCP.PaymentService.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentController : ControllerBase
{
    private static readonly Random _rng = new();

    /// <summary>Process a QR payment. 85% Approved, 10% Rejected, 5% error with simulated latency.</summary>
    [HttpPost("process")]
    public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentRequest request)
    {
        // Simulate 200ms–800ms latency
        var delay = _rng.Next(200, 801);
        await Task.Delay(delay);

        var roll = _rng.NextDouble();

        if (roll < 0.05)
        {
            return StatusCode(503, new { error = "BCP payment gateway temporary error", code = "BCP_GATEWAY_ERROR" });
        }

        var status = roll < 0.15 ? "Rejected" : "Approved";
        var shortRef = Guid.NewGuid().ToString("N")[..8].ToUpper();

        return Ok(new
        {
            transactionId = Guid.NewGuid().ToString(),
            status,
            amount = request.Amount,
            currency = request.Currency ?? "BOB",
            timestamp = DateTime.UtcNow,
            bcpReference = $"BCP-{DateTime.UtcNow:yyyyMMdd}-{shortRef}"
        });
    }

    [HttpPost("webhook")]
    public IActionResult ReceiveWebhook([FromBody] WebhookPayload payload)
    {
        return Ok(new { received = true, paymentId = Guid.NewGuid().ToString(), status = "Processed" });
    }

    [HttpGet("{paymentId}")]
    public IActionResult GetPayment(string paymentId)
    {
        return Ok(new
        {
            paymentId,
            amount = 100.00m,
            currency = "BOB",
            status = "Approved",
            timestamp = DateTime.UtcNow,
            bcpReference = $"BCP-{paymentId}"
        });
    }
}

public record ProcessPaymentRequest(string QrId, decimal Amount, string? Currency, string? IdempotencyKey);
public record WebhookPayload(string QRId, decimal Amount, string Status, DateTime Timestamp);
