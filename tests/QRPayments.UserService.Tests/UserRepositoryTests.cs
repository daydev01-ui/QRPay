// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : UserService.Tests
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-03] → [CU-03] → [UserRepositoryTests]
// Autor     : [Tesista]
// Fecha     : 2026
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using QRPayments.UserService.Application.Repositories;
using QRPayments.UserService.Domain.Entities;
using QRPayments.UserService.Domain.Enums;
using QRPayments.UserService.Infrastructure.Data;

namespace QRPayments.UserService.Tests;

public class UserRepositoryTests
{
    private static AppDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAsync_ShouldPersistUser()
    {
        // Arrange
        await using var db = CreateInMemoryDb();
        var repo = new UserRepository(db);
        var user = new AppUser { BCPUserId = "BCP001", Email = "test@test.com", FullName = "Test User", Role = UserRole.Operator, CompanyId = Guid.NewGuid() };

        // Act
        await repo.AddAsync(user);
        await repo.SaveChangesAsync();

        // Assert
        var all = await repo.GetAllAsync();
        all.Should().HaveCount(1);
        all[0].Email.Should().Be("test@test.com");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
    {
        // Arrange
        await using var db = CreateInMemoryDb();
        var repo = new UserRepository(db);

        // Act
        var found = await repo.GetByIdAsync(Guid.NewGuid());

        // Assert
        found.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUser_WhenFound()
    {
        // Arrange
        await using var db = CreateInMemoryDb();
        var repo = new UserRepository(db);
        var user = new AppUser { BCPUserId = "BCP001", Email = "user@test.com", FullName = "Test User", Role = UserRole.Operator, CompanyId = Guid.NewGuid() };
        await repo.AddAsync(user);
        await repo.SaveChangesAsync();

        // Act
        var found = await repo.GetByIdAsync(user.Id);

        // Assert
        found.Should().NotBeNull();
        found!.Email.Should().Be("user@test.com");
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyUser()
    {
        // Arrange
        await using var db = CreateInMemoryDb();
        var repo = new UserRepository(db);
        var user = new AppUser { BCPUserId = "BCP001", Email = "user@test.com", FullName = "Old Name", Role = UserRole.Operator, CompanyId = Guid.NewGuid() };
        await repo.AddAsync(user);
        await repo.SaveChangesAsync();

        // Act
        user.FullName = "New Name";
        await repo.UpdateAsync(user);
        await repo.SaveChangesAsync();

        // Assert
        var updated = await repo.GetByIdAsync(user.Id);
        updated!.FullName.Should().Be("New Name");
    }
}
