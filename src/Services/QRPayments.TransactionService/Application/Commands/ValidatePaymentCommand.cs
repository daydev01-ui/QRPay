// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : TransactionService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-05] → [CU-05] → [ValidatePaymentCommand] → [TransactionTests]
// Autor     : [Tesista]
// Fecha     : 2026
using MediatR;
using QRPayments.TransactionService.Application.Interfaces;
using QRPayments.TransactionService.Domain.Entities;
using QRPayments.TransactionService.Domain.Enums;

namespace QRPayments.TransactionService.Application.Commands;

public record ValidatePaymentCommand(
    Guid QRCodeId,
    Guid CompanyId,
    decimal Amount,
    string Currency,
    string IdempotencyKey) : IRequest<Transaction>;

public class ValidatePaymentCommandHandler : IRequestHandler<ValidatePaymentCommand, Transaction>
{
    private readonly ITransactionRepository _repo;
    private readonly IBCPPaymentClient _bcpClient;
    private readonly ILogger<ValidatePaymentCommandHandler> _logger;

    public ValidatePaymentCommandHandler(
        ITransactionRepository repo,
        IBCPPaymentClient bcpClient,
        ILogger<ValidatePaymentCommandHandler> logger)
    {
        _repo = repo;
        _bcpClient = bcpClient;
        _logger = logger;
    }

    public async Task<Transaction> Handle(ValidatePaymentCommand request, CancellationToken cancellationToken)
    {
        // IDEMPOTENCY: check if already processed
        var existing = await _repo.GetByIdempotencyKeyAsync(request.IdempotencyKey, cancellationToken);
        if (existing is not null)
        {
            _logger.LogInformation("Duplicate payment request detected. Returning existing transaction {Id}", existing.Id);
            existing.Status = TransactionStatus.Duplicate;
            await _repo.UpdateAsync(existing, cancellationToken);
            await _repo.SaveChangesAsync(cancellationToken);
            return existing;
        }

        // Create pending transaction
        var transaction = new Transaction
        {
            QRCodeId = request.QRCodeId,
            CompanyId = request.CompanyId,
            Amount = request.Amount,
            Currency = request.Currency,
            IdempotencyKey = request.IdempotencyKey,
            Status = TransactionStatus.Pending
        };

        await _repo.AddAsync(transaction, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);

        // Call BCP payment service
        try
        {
            var bcpResult = await _bcpClient.ProcessPaymentAsync(
                request.QRCodeId.ToString(),
                request.Amount,
                request.Currency,
                request.IdempotencyKey,
                cancellationToken);

            transaction.Status = bcpResult.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase)
                ? TransactionStatus.Completed
                : TransactionStatus.Failed;
            transaction.CompletedAt = DateTime.UtcNow;

            transaction.Payment = new Payment
            {
                TransactionId = transaction.Id,
                Amount = request.Amount,
                Currency = request.Currency,
                PaymentMethod = "QR",
                BCPReference = bcpResult.BcpReference
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "BCP payment call failed for transaction {Id}", transaction.Id);
            transaction.Status = TransactionStatus.Failed;
            transaction.CompletedAt = DateTime.UtcNow;
        }

        await _repo.UpdateAsync(transaction, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);
        return transaction;
    }
}
