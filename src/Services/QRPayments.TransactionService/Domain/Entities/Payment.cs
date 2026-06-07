// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : TransactionService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-05] → [CU-05] → [Payment] → [TransactionTests]
// Autor     : [Tesista]
// Fecha     : 2026
namespace QRPayments.TransactionService.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TransactionId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "BOB";
    public string PaymentMethod { get; set; } = "QR";
    public string BCPReference { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Transaction Transaction { get; set; } = null!;
}
