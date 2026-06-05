// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : ReportService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-05] → [CU-05] → [TransactionSummary] → [ReportTests]
// Autor     : [Tesista]
// Fecha     : 2026
using QRPayments.ReportService.Domain.Enums;

namespace QRPayments.ReportService.Domain.Entities;

public class TransactionSummary
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CompanyId { get; set; }
    public Guid BranchId { get; set; }
    public Guid OperatorId { get; set; }
    public decimal Amount { get; set; }
    public QRType QRType { get; set; }
    public TransactionStatus Status { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
