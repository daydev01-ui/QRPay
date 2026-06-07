// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : TransactionService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-05] → [CU-05] → [Transaction] → [TransactionTests]
// Autor     : [Tesista]
// Fecha     : 2026
using QRPayments.TransactionService.Domain.Enums;

namespace QRPayments.TransactionService.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid QRCodeId { get; set; }
    public Guid CompanyId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "BOB";
    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
    public string IdempotencyKey { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public Payment? Payment { get; set; }
}
