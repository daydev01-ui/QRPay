// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : CompanyService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-01] → [CU-01] → [CompanyRepository] → [CompanyTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.EntityFrameworkCore;
using QRPayments.CompanyService.Application.Interfaces;
using QRPayments.CompanyService.Domain.Entities;
using QRPayments.CompanyService.Infrastructure.Data;

namespace QRPayments.CompanyService.Application.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _db;

    public CompanyRepository(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<Company>> GetAllAsync(CancellationToken ct = default)
        => await _db.Companies.Include(c => c.Branches).ToListAsync(ct);

    public Task<Company?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _db.Companies.Include(c => c.Branches).FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task AddAsync(Company company, CancellationToken ct = default)
        => await _db.Companies.AddAsync(company, ct);

    public Task UpdateAsync(Company company, CancellationToken ct = default)
    {
        _db.Companies.Update(company);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Company company, CancellationToken ct = default)
    {
        _db.Companies.Remove(company);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);
}
