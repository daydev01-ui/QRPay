// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : UserService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-03] → [CU-03] → [AppDbContext] → [UserTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.EntityFrameworkCore;
using QRPayments.UserService.Domain.Entities;

namespace QRPayments.UserService.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AppUser> Users => Set<AppUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppUser>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.Email).IsRequired().HasMaxLength(200);
            e.Property(u => u.FullName).IsRequired().HasMaxLength(200);
            e.Property(u => u.BCPUserId).IsRequired().HasMaxLength(100);
            e.HasIndex(u => u.Email).IsUnique();
        });
    }
}
