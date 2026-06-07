// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : UserService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-03] → [CU-03] → [UsersController] → [UserTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRPayments.UserService.Application.Interfaces;
using QRPayments.UserService.Domain.Entities;
using QRPayments.UserService.Domain.Enums;

namespace QRPayments.UserService.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repo;

    public UsersController(IUserRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var users = await _repo.GetAllAsync(ct);
        return Ok(users.Select(MapToDto));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var user = await _repo.GetByIdAsync(id, ct);
        if (user is null) return NotFound();
        return Ok(MapToDto(user));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken ct)
    {
        var user = new AppUser
        {
            BCPUserId = request.BCPUserId,
            Email = request.Email,
            FullName = request.FullName,
            Role = request.Role,
            CompanyId = request.CompanyId,
            BranchId = request.BranchId
        };
        await _repo.AddAsync(user, ct);
        await _repo.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, MapToDto(user));
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request, CancellationToken ct)
    {
        var user = await _repo.GetByIdAsync(id, ct);
        if (user is null) return NotFound();

        user.FullName = request.FullName ?? user.FullName;
        user.Email = request.Email ?? user.Email;
        if (request.Role.HasValue) user.Role = request.Role.Value;
        if (request.Status.HasValue) user.Status = request.Status.Value;
        if (request.BranchId.HasValue) user.BranchId = request.BranchId.Value;

        await _repo.UpdateAsync(user, ct);
        await _repo.SaveChangesAsync(ct);
        return Ok(MapToDto(user));
    }

    private static UserDto MapToDto(AppUser u) => new(
        u.Id, u.BCPUserId, u.Email, u.FullName,
        u.Role.ToString(), u.CompanyId, u.BranchId,
        u.Status.ToString(), u.CreatedAt);
}

public record CreateUserRequest(string BCPUserId, string Email, string FullName, UserRole Role, Guid CompanyId, Guid? BranchId);
public record UpdateUserRequest(string? FullName, string? Email, UserRole? Role, UserStatus? Status, Guid? BranchId);
public record UserDto(Guid Id, string BCPUserId, string Email, string FullName, string Role, Guid CompanyId, Guid? BranchId, string Status, DateTime CreatedAt);
