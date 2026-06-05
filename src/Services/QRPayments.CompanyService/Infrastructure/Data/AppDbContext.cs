// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : CompanyService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-01] → [CU-01] → [AppDbContext] → [CompanyTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.EntityFrameworkCore;
using QRPayments.CompanyService.Domain.Entities;

namespace QRPayments.CompanyService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Branch> Branches => Set<Branch>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Company>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Name).IsRequired().HasMaxLength(200);
            e.Property(c => c.BCPAccountNumber).IsRequired().HasMaxLength(50);
            e.Property(c => c.RUC).IsRequired().HasMaxLength(20);
            e.HasMany(c => c.Branches).WithOne(b => b.Company).HasForeignKey(b => b.CompanyId);
        });

        modelBuilder.Entity<Branch>(e =>
        {
            e.HasKey(b => b.Id);
            e.Property(b => b.Name).IsRequired().HasMaxLength(200);
            e.Property(b => b.Address).IsRequired().HasMaxLength(500);
        });
    }
}
