// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : Shared.Contracts
// Iteración : Fase 3 — Construcción, Iteración 1
// Autor     : [Tesista]
// Fecha     : 2026
namespace QRPayments.Shared.Contracts.DTOs;

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
