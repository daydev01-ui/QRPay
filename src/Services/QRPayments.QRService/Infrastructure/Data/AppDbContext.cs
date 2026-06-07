// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : QRService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-04] → [CU-04] → [AppDbContext] → [QRTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.EntityFrameworkCore;
using QRPayments.QRService.Domain.Entities;

namespace QRPayments.QRService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<QRCode> QRCodes => Set<QRCode>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<QRCode>(e =>
        {
            e.HasKey(q => q.Id);
            e.Property(q => q.Code).IsRequired().HasMaxLength(500);
            e.Property(q => q.Currency).IsRequired().HasMaxLength(10);
            e.Property(q => q.Amount).HasColumnType("decimal(18,2)");
            e.HasIndex(q => q.Code).IsUnique();
        });
    }
}
