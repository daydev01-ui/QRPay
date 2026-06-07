// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : CompanyService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-01] → [CU-01] → [CompaniesController] → [CompanyTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRPayments.CompanyService.Application.Interfaces;
using QRPayments.CompanyService.Domain.Entities;
using QRPayments.CompanyService.Domain.Enums;

namespace QRPayments.CompanyService.Controllers;

[ApiController]
[Route("api/companies")]
[Authorize]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyRepository _repo;

    public CompaniesController(ICompanyRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var companies = await _repo.GetAllAsync(ct);
        return Ok(companies.Select(MapToDto));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var company = await _repo.GetByIdAsync(id, ct);
        if (company is null) return NotFound();
        return Ok(MapToDto(company));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCompanyRequest request, CancellationToken ct)
    {
        var company = new Company
        {
            Name = request.Name,
            BCPAccountNumber = request.BCPAccountNumber,
            RUC = request.RUC
        };
        await _repo.AddAsync(company, ct);
        await _repo.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = company.Id }, MapToDto(company));
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCompanyRequest request, CancellationToken ct)
    {
        var company = await _repo.GetByIdAsync(id, ct);
        if (company is null) return NotFound();

        company.Name = request.Name ?? company.Name;
        company.BCPAccountNumber = request.BCPAccountNumber ?? company.BCPAccountNumber;
        company.RUC = request.RUC ?? company.RUC;
        if (request.Status.HasValue) company.Status = request.Status.Value;

        await _repo.UpdateAsync(company, ct);
        await _repo.SaveChangesAsync(ct);
        return Ok(MapToDto(company));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var company = await _repo.GetByIdAsync(id, ct);
        if (company is null) return NotFound();
        await _repo.DeleteAsync(company, ct);
        await _repo.SaveChangesAsync(ct);
        return NoContent();
    }

    private static CompanyDto MapToDto(Company c) => new(
        c.Id, c.Name, c.BCPAccountNumber, c.RUC,
        c.Status.ToString(), c.CreatedAt,
        c.Branches.Select(b => new BranchDto(b.Id, b.Name, b.Address)).ToList());
}

public record CreateCompanyRequest(string Name, string BCPAccountNumber, string RUC);
public record UpdateCompanyRequest(string? Name, string? BCPAccountNumber, string? RUC, CompanyStatus? Status);
public record BranchDto(Guid Id, string Name, string Address);
public record CompanyDto(Guid Id, string Name, string BCPAccountNumber, string RUC, string Status, DateTime CreatedAt, List<BranchDto> Branches);
