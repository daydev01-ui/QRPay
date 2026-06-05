// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : NotificationService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-04] → [CU-04] → [Notification] → [NotificationTests]
// Autor     : [Tesista]
// Fecha     : 2026
using QRPayments.NotificationService.Domain.Enums;

namespace QRPayments.NotificationService.Domain.Entities;

public class Notification
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? PaymentRef { get; set; }
}
