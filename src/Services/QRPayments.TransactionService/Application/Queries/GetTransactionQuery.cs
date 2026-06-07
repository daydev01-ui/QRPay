// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : TransactionService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-05] → [CU-05] → [GetTransactionQuery] → [TransactionTests]
// Autor     : [Tesista]
// Fecha     : 2026
using MediatR;
using QRPayments.TransactionService.Application.Interfaces;
using QRPayments.TransactionService.Domain.Entities;

namespace QRPayments.TransactionService.Application.Queries;

public record GetTransactionQuery(Guid Id) : IRequest<Transaction?>;

public class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, Transaction?>
{
    private readonly ITransactionRepository _repo;

    public GetTransactionQueryHandler(ITransactionRepository repo) => _repo = repo;

    public Task<Transaction?> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        => _repo.GetByIdAsync(request.Id, cancellationToken);
}
