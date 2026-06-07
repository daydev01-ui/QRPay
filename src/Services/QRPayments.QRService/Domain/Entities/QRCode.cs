// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : QRService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-04] → [CU-04] → [QRCode] → [QRTests]
// Autor     : [Tesista]
// Fecha     : 2026
using QRPayments.QRService.Domain.Enums;

namespace QRPayments.QRService.Domain.Entities;

public class QRCode
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "BOB";
    public Guid CompanyId { get; set; }
    public QRStatus Status { get; set; } = QRStatus.Active;
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    /// <summary>Base64-encoded PNG of the QR image.</summary>
    public string? ImageBase64 { get; set; }
}
