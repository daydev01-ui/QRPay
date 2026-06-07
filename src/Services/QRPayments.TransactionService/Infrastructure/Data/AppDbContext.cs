// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : TransactionService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-05] → [CU-05] → [AppDbContext] → [TransactionTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.EntityFrameworkCore;
using QRPayments.TransactionService.Domain.Entities;

namespace QRPayments.TransactionService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transaction>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.Currency).IsRequired().HasMaxLength(10);
            e.Property(t => t.Amount).HasColumnType("decimal(18,2)");
            e.Property(t => t.IdempotencyKey).IsRequired().HasMaxLength(200);
            e.HasIndex(t => t.IdempotencyKey).IsUnique();
            e.HasOne(t => t.Payment).WithOne(p => p.Transaction)
                .HasForeignKey<Payment>(p => p.TransactionId);
        });

        modelBuilder.Entity<Payment>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Currency).IsRequired().HasMaxLength(10);
            e.Property(p => p.Amount).HasColumnType("decimal(18,2)");
            e.Property(p => p.BCPReference).IsRequired().HasMaxLength(100);
        });
    }
}
