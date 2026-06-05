// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : ReportService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-05] → [CU-05] → [TransactionStatus] → [ReportTests]
// Autor     : [Tesista]
// Fecha     : 2026
namespace QRPayments.ReportService.Domain.Enums;

public enum TransactionStatus
{
    Pending = 1,
    Completed = 2,
    Failed = 3,
    Reversed = 4
}
