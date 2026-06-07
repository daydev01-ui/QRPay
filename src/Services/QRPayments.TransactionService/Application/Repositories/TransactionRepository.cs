// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : TransactionService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-05] → [CU-05] → [TransactionRepository] → [TransactionTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.EntityFrameworkCore;
using QRPayments.TransactionService.Application.Interfaces;
using QRPayments.TransactionService.Domain.Entities;
using QRPayments.TransactionService.Domain.Enums;
using QRPayments.TransactionService.Infrastructure.Data;

namespace QRPayments.TransactionService.Application.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _db;

    public TransactionRepository(AppDbContext db) => _db = db;

    public Task<Transaction?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.Transactions.Include(t => t.Payment).FirstOrDefaultAsync(t => t.Id == id, ct);

    public Task<Transaction?> GetByIdempotencyKeyAsync(string key, CancellationToken ct = default)
        => _db.Transactions.Include(t => t.Payment).FirstOrDefaultAsync(t => t.IdempotencyKey == key, ct);

    public async Task<IReadOnlyList<Transaction>> GetByCompanyIdAsync(Guid companyId, TransactionStatus? status, CancellationToken ct = default)
    {
        var query = _db.Transactions.Include(t => t.Payment)
            .Where(t => t.CompanyId == companyId);
        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);
        return await query.OrderByDescending(t => t.CreatedAt).ToListAsync(ct);
    }

    public async Task AddAsync(Transaction transaction, CancellationToken ct = default)
        => await _db.Transactions.AddAsync(transaction, ct);

    public Task UpdateAsync(Transaction transaction, CancellationToken ct = default)
    {
        _db.Transactions.Update(transaction);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);
}
