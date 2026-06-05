// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : MockBCP.QRService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-00] → [CU-00] → [QRController] → [IntegrationTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.AspNetCore.Mvc;

namespace MockBCP.QRService.Controllers;

[ApiController]
[Route("api/qr")]
public class QRController : ControllerBase
{
    [HttpPost("generate")]
    public IActionResult GenerateQR([FromBody] QRRequest request)
    {
        return Ok(new
        {
            qrId = Guid.NewGuid().ToString(),
            qrCode = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"QR-MOCK-{request.Amount}-{DateTime.UtcNow:yyyyMMddHHmmss}")),
            amount = request.Amount,
            currency = "BOB",
            expiresAt = DateTime.UtcNow.AddMinutes(30),
            status = "Generated"
        });
    }

    [HttpGet("{qrId}/status")]
    public IActionResult GetQRStatus(string qrId)
    {
        return Ok(new { qrId, status = "Active", scannedAt = (DateTime?)null });
    }
}

public record QRRequest(decimal Amount, string Description, string CompanyId);
