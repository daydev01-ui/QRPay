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
    private static readonly Random _rng = new();

    /// <summary>Validate a QR code for payment. Returns validity, merchant info, and amount.</summary>
    [HttpPost("validate")]
    public async Task<IActionResult> ValidateQR([FromBody] QRValidateRequest request)
    {
        // Simulate 200ms–800ms latency
        var delay = _rng.Next(200, 801);
        await Task.Delay(delay);

        // Simulate basic validation: codes starting with "QRP|" are considered valid
        var isValid = !string.IsNullOrWhiteSpace(request.QrCode) && request.QrCode.StartsWith("QRP|");

        if (!isValid)
        {
            return Ok(new { valid = false, error = "QR code is invalid or expired" });
        }

        // Parse code data: QRP|{companyId}|{amount}|{currency}|...
        var parts = request.QrCode.Split('|');
        var companyIdPart = parts.Length > 1 ? parts[1] : "unknown";
        var merchantName = $"Company-{companyIdPart[..Math.Min(8, companyIdPart.Length)]}";
        var amount = parts.Length > 2 && decimal.TryParse(parts[2], out var a) ? a : request.Amount;
        var currency = parts.Length > 3 ? parts[3] : "BOB";

        return Ok(new
        {
            valid = true,
            qrId = Guid.NewGuid().ToString(),
            merchantName,
            amount,
            currency,
            expiresAt = DateTime.UtcNow.AddMinutes(30)
        });
    }

    /// <summary>Get QR code details by ID.</summary>
    [HttpGet("{qrId}")]
    public async Task<IActionResult> GetQR(string qrId)
    {
        var delay = _rng.Next(200, 801);
        await Task.Delay(delay);

        return Ok(new
        {
            qrId,
            status = "Active",
            amount = 100.00m,
            currency = "BOB",
            merchantName = $"Merchant-{qrId[..Math.Min(8, qrId.Length)]}",
            createdAt = DateTime.UtcNow.AddMinutes(-5),
            expiresAt = DateTime.UtcNow.AddMinutes(25)
        });
    }

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

public record QRValidateRequest(string QrCode, decimal Amount);
public record QRRequest(decimal Amount, string Description, string CompanyId);
