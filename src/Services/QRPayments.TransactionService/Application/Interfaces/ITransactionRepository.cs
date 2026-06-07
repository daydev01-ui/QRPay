// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : TransactionService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-05] → [CU-05] → [ITransactionRepository] → [TransactionTests]
// Autor     : [Tesista]
// Fecha     : 2026
using QRPayments.TransactionService.Domain.Entities;
using QRPayments.TransactionService.Domain.Enums;

namespace QRPayments.TransactionService.Application.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Transaction?> GetByIdempotencyKeyAsync(string key, CancellationToken ct = default);
    Task<IReadOnlyList<Transaction>> GetByCompanyIdAsync(Guid companyId, TransactionStatus? status, CancellationToken ct = default);
    Task AddAsync(Transaction transaction, CancellationToken ct = default);
    Task UpdateAsync(Transaction transaction, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
