// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : CompanyService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-02] → [CU-02] → [Branch] → [CompanyTests]
// Autor     : [Tesista]
// Fecha     : 2026
using QRPayments.CompanyService.Domain.Enums;

namespace QRPayments.CompanyService.Domain.Entities;

public class Branch
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public CompanyStatus Status { get; set; } = CompanyStatus.Active;
    public Company Company { get; set; } = null!;
}
