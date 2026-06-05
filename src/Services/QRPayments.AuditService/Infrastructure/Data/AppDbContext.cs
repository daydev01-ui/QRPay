// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : AuditService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-06] → [CU-06] → [AppDbContext] → [AuditTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.EntityFrameworkCore;
using QRPayments.AuditService.Domain.Entities;

namespace QRPayments.AuditService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AuditLog>(e =>
        {
            e.HasKey(a => a.Id);
            e.Property(a => a.Action).IsRequired().HasMaxLength(100);
            e.Property(a => a.Entity).IsRequired().HasMaxLength(100);
            e.Property(a => a.Details).HasMaxLength(2000);
            e.Property(a => a.IPAddress).HasMaxLength(50);
        });
    }
}
