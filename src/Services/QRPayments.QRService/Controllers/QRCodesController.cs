// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : QRService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-04] → [CU-04] → [QRCodesController] → [QRTests]
// Autor     : [Tesista]
// Fecha     : 2026
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRPayments.QRService.Application.Commands;
using QRPayments.QRService.Application.Interfaces;
using QRPayments.QRService.Application.Queries;
using QRPayments.QRService.Domain.Enums;

namespace QRPayments.QRService.Controllers;

[ApiController]
[Route("api/qrcodes")]
[Authorize]
public class QRCodesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IQRRepository _repo;

    public QRCodesController(IMediator mediator, IQRRepository repo)
    {
        _mediator = mediator;
        _repo = repo;
    }

    /// <summary>Generate a new QR code for a company.</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateQRRequest request)
    {
        var qr = await _mediator.Send(new GenerateQRCommand(
            request.CompanyId, request.Amount, request.Currency ?? "BOB", request.ExpiresInMinutes ?? 30));
        return CreatedAtAction(nameof(GetById), new { id = qr.Id }, MapToDto(qr));
    }

    /// <summary>Get QR code by ID.</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var qr = await _mediator.Send(new GetQRByIdQuery(id));
        if (qr is null) return NotFound();
        return Ok(MapToDto(qr));
    }

    /// <summary>Get all QR codes; optionally filter by companyId.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? companyId)
    {
        if (companyId.HasValue)
        {
            var list = await _repo.GetByCompanyIdAsync(companyId.Value);
            return Ok(list.Select(MapToDto));
        }
        return BadRequest("companyId query parameter is required.");
    }

    /// <summary>Disable a QR code.</summary>
    [HttpPut("{id:guid}/disable")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Disable(Guid id)
    {
        var qr = await _repo.GetByIdAsync(id);
        if (qr is null) return NotFound();
        qr.Status = QRStatus.Disabled;
        await _repo.UpdateAsync(qr);
        await _repo.SaveChangesAsync();
        return Ok(MapToDto(qr));
    }

    private static QRCodeDto MapToDto(Domain.Entities.QRCode q) => new(
        q.Id, q.Code, q.Amount, q.Currency, q.CompanyId,
        q.Status.ToString(), q.ExpiresAt, q.CreatedAt, q.ImageBase64);
}

public record CreateQRRequest(Guid CompanyId, decimal Amount, string? Currency, int? ExpiresInMinutes);

public record QRCodeDto(
    Guid Id,
    string Code,
    decimal Amount,
    string Currency,
    Guid CompanyId,
    string Status,
    DateTime ExpiresAt,
    DateTime CreatedAt,
    string? ImageBase64);
