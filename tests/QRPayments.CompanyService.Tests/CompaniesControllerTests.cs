// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : CompanyService.Tests
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-01] → [CU-01] → [CompaniesControllerTests]
// Autor     : [Tesista]
// Fecha     : 2026
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using QRPayments.CompanyService.Application.Interfaces;
using QRPayments.CompanyService.Controllers;
using QRPayments.CompanyService.Domain.Entities;
using QRPayments.CompanyService.Domain.Enums;

namespace QRPayments.CompanyService.Tests;

public class CompaniesControllerTests
{
    private readonly Mock<ICompanyRepository> _repoMock = new();
    private readonly CompaniesController _controller;

    public CompaniesControllerTests()
    {
        _controller = new CompaniesController(_repoMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOkWithCompanies()
    {
        // Arrange
        var companies = new List<Company>
        {
            new() { Name = "Corp A", BCPAccountNumber = "ACC001", RUC = "11111111111" },
            new() { Name = "Corp B", BCPAccountNumber = "ACC002", RUC = "22222222222" }
        };
        _repoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(companies);

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenFound()
    {
        // Arrange
        var company = new Company { Name = "Corp A", BCPAccountNumber = "ACC001", RUC = "11111111111" };
        _repoMock.Setup(r => r.GetByIdAsync(company.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(company);

        // Act
        var result = await _controller.GetById(company.Id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenMissing()
    {
        // Arrange
        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Company?)null);

        // Act
        var result = await _controller.GetById(Guid.NewGuid(), CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedAtAction()
    {
        // Arrange
        _repoMock.Setup(r => r.AddAsync(It.IsAny<Company>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _repoMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var request = new CreateCompanyRequest("Test Corp", "ACC001", "12345678901");

        // Act
        var result = await _controller.Create(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
        _repoMock.Verify(r => r.AddAsync(It.IsAny<Company>(), It.IsAny<CancellationToken>()), Times.Once);
        _repoMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Update_ShouldReturnNotFound_WhenMissing()
    {
        // Arrange
        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Company?)null);

        // Act
        var result = await _controller.Update(Guid.NewGuid(), new UpdateCompanyRequest(null, null, null, null), CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenFound()
    {
        // Arrange
        var company = new Company { Name = "Corp A", BCPAccountNumber = "ACC001", RUC = "11111111111" };
        _repoMock.Setup(r => r.GetByIdAsync(company.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(company);
        _repoMock.Setup(r => r.DeleteAsync(It.IsAny<Company>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _repoMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(company.Id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }
}
