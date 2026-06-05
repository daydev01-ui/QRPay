// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : AuditService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-06] → [CU-06] → [AuditLog] → [AuditTests]
// Autor     : [Tesista]
// Fecha     : 2026
namespace QRPayments.AuditService.Domain.Entities;

public class AuditLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string Entity { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public string IPAddress { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
