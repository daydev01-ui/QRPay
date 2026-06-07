// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : QRService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-04] → [CU-04] → [GetQRByIdQuery] → [QRTests]
// Autor     : [Tesista]
// Fecha     : 2026
using MediatR;
using QRPayments.QRService.Application.Interfaces;
using QRPayments.QRService.Domain.Entities;

namespace QRPayments.QRService.Application.Queries;

public record GetQRByIdQuery(Guid Id) : IRequest<QRCode?>;

public class GetQRByIdQueryHandler : IRequestHandler<GetQRByIdQuery, QRCode?>
{
    private readonly IQRRepository _repo;

    public GetQRByIdQueryHandler(IQRRepository repo) => _repo = repo;

    public Task<QRCode?> Handle(GetQRByIdQuery request, CancellationToken cancellationToken)
        => _repo.GetByIdAsync(request.Id, cancellationToken);
}
