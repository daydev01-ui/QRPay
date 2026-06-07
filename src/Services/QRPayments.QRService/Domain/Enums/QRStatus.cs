// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : QRService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-04] → [CU-04] → [QRStatus] → [QRTests]
// Autor     : [Tesista]
// Fecha     : 2026
namespace QRPayments.QRService.Domain.Enums;

public enum QRStatus
{
    Active,
    Expired,
    Disabled
}
