// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : NotificationService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-04] → [CU-04] → [AppDbContext] → [NotificationTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.EntityFrameworkCore;
using QRPayments.NotificationService.Domain.Entities;

namespace QRPayments.NotificationService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Notification>(e =>
        {
            e.HasKey(n => n.Id);
            e.Property(n => n.Message).IsRequired().HasMaxLength(1000);
            e.Property(n => n.PaymentRef).HasMaxLength(100);
        });
    }
}
