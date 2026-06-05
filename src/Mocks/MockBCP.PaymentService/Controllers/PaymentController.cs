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
            status = "Completed",
            timestamp = DateTime.UtcNow,
            qrId = Guid.NewGuid().ToString()
        });
    }
}

public record WebhookPayload(string QRId, decimal Amount, string Status, DateTime Timestamp);
