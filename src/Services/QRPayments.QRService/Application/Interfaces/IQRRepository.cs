// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : QRService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-04] → [CU-04] → [IQRRepository] → [QRTests]
// Autor     : [Tesista]
// Fecha     : 2026
using QRPayments.QRService.Domain.Entities;

namespace QRPayments.QRService.Application.Interfaces;

public interface IQRRepository
{
    Task<QRCode?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<QRCode?> GetByCodeAsync(string code, CancellationToken ct = default);
    Task<IReadOnlyList<QRCode>> GetByCompanyIdAsync(Guid companyId, CancellationToken ct = default);
    Task AddAsync(QRCode qr, CancellationToken ct = default);
    Task UpdateAsync(QRCode qr, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
