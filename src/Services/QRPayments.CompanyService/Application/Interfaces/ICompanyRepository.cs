// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : CompanyService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-01] → [CU-01] → [ICompanyRepository] → [CompanyTests]
// Autor     : [Tesista]
// Fecha     : 2026
using QRPayments.CompanyService.Domain.Entities;

namespace QRPayments.CompanyService.Application.Interfaces;

public interface ICompanyRepository
{
    Task<IReadOnlyList<Company>> GetAllAsync(CancellationToken ct = default);
    Task<Company?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Company company, CancellationToken ct = default);
    Task UpdateAsync(Company company, CancellationToken ct = default);
    Task DeleteAsync(Company company, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
