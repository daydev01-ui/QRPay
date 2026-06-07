// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : CompanyService.Tests
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-01] → [CU-01] → [CompanyRepositoryTests]
// Autor     : [Tesista]
// Fecha     : 2026
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using QRPayments.CompanyService.Application.Repositories;
using QRPayments.CompanyService.Domain.Entities;
using QRPayments.CompanyService.Domain.Enums;
using QRPayments.CompanyService.Infrastructure.Data;

namespace QRPayments.CompanyService.Tests;

public class CompanyRepositoryTests
{
    private static AppDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAsync_ShouldPersistCompany()
    {
        // Arrange
        await using var db = CreateInMemoryDb();
        var repo = new CompanyRepository(db);
        var company = new Company { Name = "Test Corp", BCPAccountNumber = "ACC001", RUC = "12345678901" };

        // Act
        await repo.AddAsync(company);
        await repo.SaveChangesAsync();

        // Assert
        var all = await repo.GetAllAsync();
        all.Should().HaveCount(1);
        all[0].Name.Should().Be("Test Corp");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectCompany()
    {
        // Arrange
        await using var db = CreateInMemoryDb();
        var repo = new CompanyRepository(db);
        var company = new Company { Name = "Corp A", BCPAccountNumber = "ACC001", RUC = "12345678901" };
        await repo.AddAsync(company);
        await repo.SaveChangesAsync();

        // Act
        var found = await repo.GetByIdAsync(company.Id);

        // Assert
        found.Should().NotBeNull();
        found!.Name.Should().Be("Corp A");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
    {
        // Arrange
        await using var db = CreateInMemoryDb();
        var repo = new CompanyRepository(db);

        // Act
        var found = await repo.GetByIdAsync(Guid.NewGuid());

        // Assert
        found.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_ShouldModifyCompany()
    {
        // Arrange
        await using var db = CreateInMemoryDb();
        var repo = new CompanyRepository(db);
        var company = new Company { Name = "Old Name", BCPAccountNumber = "ACC001", RUC = "12345678901" };
        await repo.AddAsync(company);
        await repo.SaveChangesAsync();

        // Act
        company.Name = "New Name";
        await repo.UpdateAsync(company);
        await repo.SaveChangesAsync();

        // Assert
        var updated = await repo.GetByIdAsync(company.Id);
        updated!.Name.Should().Be("New Name");
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveCompany()
    {
        // Arrange
        await using var db = CreateInMemoryDb();
        var repo = new CompanyRepository(db);
        var company = new Company { Name = "To Delete", BCPAccountNumber = "ACC001", RUC = "12345678901" };
        await repo.AddAsync(company);
        await repo.SaveChangesAsync();

        // Act
        await repo.DeleteAsync(company);
        await repo.SaveChangesAsync();

        // Assert
        var all = await repo.GetAllAsync();
        all.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCompanies()
    {
        // Arrange
        await using var db = CreateInMemoryDb();
        var repo = new CompanyRepository(db);
        await repo.AddAsync(new Company { Name = "Corp A", BCPAccountNumber = "ACC001", RUC = "11111111111" });
        await repo.AddAsync(new Company { Name = "Corp B", BCPAccountNumber = "ACC002", RUC = "22222222222" });
        await repo.SaveChangesAsync();

        // Act
        var all = await repo.GetAllAsync();

        // Assert
        all.Should().HaveCount(2);
    }
}
