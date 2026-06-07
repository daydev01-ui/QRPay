// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : TransactionService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-05] → [CU-05] → [TransactionStatus] → [TransactionTests]
// Autor     : [Tesista]
// Fecha     : 2026
namespace QRPayments.TransactionService.Domain.Enums;

public enum TransactionStatus
{
    Pending,
    Completed,
    Failed,
    Duplicate
}
