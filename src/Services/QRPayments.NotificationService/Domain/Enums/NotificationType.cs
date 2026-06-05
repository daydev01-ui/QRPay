// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : NotificationService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-04] → [CU-04] → [NotificationType] → [NotificationTests]
// Autor     : [Tesista]
// Fecha     : 2026
namespace QRPayments.NotificationService.Domain.Enums;

public enum NotificationType
{
    PaymentReceived = 1,
    PaymentFailed = 2,
    QRGenerated = 3,
    SystemAlert = 4
}
