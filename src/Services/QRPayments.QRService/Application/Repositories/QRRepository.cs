// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : QRService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-04] → [CU-04] → [QRRepository] → [QRTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.EntityFrameworkCore;
using QRPayments.QRService.Application.Interfaces;
using QRPayments.QRService.Domain.Entities;
using QRPayments.QRService.Infrastructure.Data;

namespace QRPayments.QRService.Application.Repositories;

public class QRRepository : IQRRepository
{
    private readonly AppDbContext _db;

    public QRRepository(AppDbContext db) => _db = db;

    public Task<QRCode?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.QRCodes.FirstOrDefaultAsync(q => q.Id == id, ct);

    public Task<QRCode?> GetByCodeAsync(string code, CancellationToken ct = default)
        => _db.QRCodes.FirstOrDefaultAsync(q => q.Code == code, ct);

    public async Task<IReadOnlyList<QRCode>> GetByCompanyIdAsync(Guid companyId, CancellationToken ct = default)
        => await _db.QRCodes.Where(q => q.CompanyId == companyId).ToListAsync(ct);

    public async Task AddAsync(QRCode qr, CancellationToken ct = default)
        => await _db.QRCodes.AddAsync(qr, ct);

    public Task UpdateAsync(QRCode qr, CancellationToken ct = default)
    {
        _db.QRCodes.Update(qr);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);
}
