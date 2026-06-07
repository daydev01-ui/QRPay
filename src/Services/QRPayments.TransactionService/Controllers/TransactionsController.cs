// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : TransactionService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-05] → [CU-05] → [TransactionsController] → [TransactionTests]
// Autor     : [Tesista]
// Fecha     : 2026
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRPayments.TransactionService.Application.Commands;
using QRPayments.TransactionService.Application.Interfaces;
using QRPayments.TransactionService.Application.Queries;
using QRPayments.TransactionService.Domain.Enums;

namespace QRPayments.TransactionService.Controllers;

[ApiController]
[Route("api/transactions")]
[Authorize]
public class TransactionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITransactionRepository _repo;

    public TransactionsController(IMediator mediator, ITransactionRepository repo)
    {
        _mediator = mediator;
        _repo = repo;
    }

    /// <summary>Validate and process a QR payment.</summary>
    [HttpPost("validate")]
    public async Task<IActionResult> ValidatePayment([FromBody] ValidatePaymentRequest request, CancellationToken ct)
    {
        var transaction = await _mediator.Send(new ValidatePaymentCommand(
            request.QRCodeId,
            request.CompanyId,
            request.Amount,
            request.Currency ?? "BOB",
            request.IdempotencyKey), ct);

        return Ok(MapToDto(transaction));
    }

    /// <summary>Get transaction by ID.</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var transaction = await _mediator.Send(new GetTransactionQuery(id), ct);
        if (transaction is null) return NotFound();
        return Ok(MapToDto(transaction));
    }

    /// <summary>Get transactions by company, with optional status filter.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid companyId, [FromQuery] TransactionStatus? status, CancellationToken ct)
    {
        if (companyId == Guid.Empty) return BadRequest("companyId is required.");
        var list = await _repo.GetByCompanyIdAsync(companyId, status, ct);
        return Ok(list.Select(MapToDto));
    }

    private static TransactionDto MapToDto(Domain.Entities.Transaction t) => new(
        t.Id, t.QRCodeId, t.CompanyId, t.Amount, t.Currency,
        t.Status.ToString(), t.IdempotencyKey, t.CreatedAt, t.CompletedAt,
        t.Payment is null ? null : new PaymentDto(
            t.Payment.Id, t.Payment.Amount, t.Payment.Currency,
            t.Payment.PaymentMethod, t.Payment.BCPReference, t.Payment.CreatedAt));
}

public record ValidatePaymentRequest(Guid QRCodeId, Guid CompanyId, decimal Amount, string? Currency, string IdempotencyKey);
public record PaymentDto(Guid Id, decimal Amount, string Currency, string PaymentMethod, string BCPReference, DateTime CreatedAt);
public record TransactionDto(
    Guid Id, Guid QRCodeId, Guid CompanyId, decimal Amount, string Currency,
    string Status, string IdempotencyKey, DateTime CreatedAt, DateTime? CompletedAt, PaymentDto? Payment);
