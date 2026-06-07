// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : ReportService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-06] → [CU-06] → [AppDbContext] → [ReportTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.EntityFrameworkCore;
using QRPayments.ReportService.Domain.Entities;

namespace QRPayments.ReportService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TransactionSummary> TransactionSummaries => Set<TransactionSummary>();
    public DbSet<Report> Reports => Set<Report>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TransactionSummary>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.Amount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Report>(e =>
        {
            e.HasKey(r => r.Id);
            e.Property(r => r.GeneratedBy).IsRequired().HasMaxLength(200);
            e.Property(r => r.FilePath).HasMaxLength(500);
        });
    }
}
