// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : UserService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-03] → [CU-03] → [UserRepository] → [UserTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.EntityFrameworkCore;
using QRPayments.UserService.Application.Interfaces;
using QRPayments.UserService.Domain.Entities;
using QRPayments.UserService.Infrastructure.Data;

namespace QRPayments.UserService.Application.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<AppUser>> GetAllAsync(CancellationToken ct = default)
        => await _db.Users.ToListAsync(ct);

    public Task<AppUser?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);

    public async Task AddAsync(AppUser user, CancellationToken ct = default)
        => await _db.Users.AddAsync(user, ct);

    public Task UpdateAsync(AppUser user, CancellationToken ct = default)
    {
        _db.Users.Update(user);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);
}
