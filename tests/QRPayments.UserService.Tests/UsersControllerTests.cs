// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : UserService.Tests
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-03] → [CU-03] → [UsersControllerTests]
// Autor     : [Tesista]
// Fecha     : 2026
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using QRPayments.UserService.Application.Interfaces;
using QRPayments.UserService.Controllers;
using QRPayments.UserService.Domain.Entities;
using QRPayments.UserService.Domain.Enums;

namespace QRPayments.UserService.Tests;

public class UsersControllerTests
{
    private readonly Mock<IUserRepository> _repoMock = new();
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _controller = new UsersController(_repoMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOkWithUsers()
    {
        // Arrange
        var users = new List<AppUser>
        {
            new() { BCPUserId = "BCP001", Email = "a@a.com", FullName = "User A", Role = UserRole.Operator, CompanyId = Guid.NewGuid() },
            new() { BCPUserId = "BCP002", Email = "b@b.com", FullName = "User B", Role = UserRole.Admin, CompanyId = Guid.NewGuid() }
        };
        _repoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        // Act
        var result = await _controller.GetAll(CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenFound()
    {
        // Arrange
        var user = new AppUser { BCPUserId = "BCP001", Email = "a@a.com", FullName = "User A", Role = UserRole.Operator, CompanyId = Guid.NewGuid() };
        _repoMock.Setup(r => r.GetByIdAsync(user.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        var result = await _controller.GetById(user.Id, CancellationToken.None);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenMissing()
    {
        // Arrange
        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AppUser?)null);

        // Act
        var result = await _controller.GetById(Guid.NewGuid(), CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedAtAction()
    {
        // Arrange
        _repoMock.Setup(r => r.AddAsync(It.IsAny<AppUser>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _repoMock.Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var request = new CreateUserRequest("BCP001", "a@a.com", "User A", UserRole.Operator, Guid.NewGuid(), null);

        // Act
        var result = await _controller.Create(request, CancellationToken.None);

        // Assert
        result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task Update_ShouldReturnNotFound_WhenMissing()
    {
        // Arrange
        _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AppUser?)null);

        // Act
        var result = await _controller.Update(Guid.NewGuid(), new UpdateUserRequest(null, null, null, null, null), CancellationToken.None);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}
