// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : UserService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-03] → [CU-03] → [IUserRepository] → [UserTests]
// Autor     : [Tesista]
// Fecha     : 2026
using QRPayments.UserService.Domain.Entities;

namespace QRPayments.UserService.Application.Interfaces;

public interface IUserRepository
{
    Task<IReadOnlyList<AppUser>> GetAllAsync(CancellationToken ct = default);
    Task<AppUser?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(AppUser user, CancellationToken ct = default);
    Task UpdateAsync(AppUser user, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
