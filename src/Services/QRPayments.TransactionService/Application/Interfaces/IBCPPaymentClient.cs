// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : TransactionService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-05] → [CU-05] → [IBCPPaymentClient] → [TransactionTests]
// Autor     : [Tesista]
// Fecha     : 2026
namespace QRPayments.TransactionService.Application.Interfaces;

public interface IBCPPaymentClient
{
    Task<BCPPaymentResult> ProcessPaymentAsync(string qrId, decimal amount, string currency, string idempotencyKey, CancellationToken ct = default);
}

public record BCPPaymentResult(string TransactionId, string Status, decimal Amount, DateTime Timestamp, string BcpReference);
