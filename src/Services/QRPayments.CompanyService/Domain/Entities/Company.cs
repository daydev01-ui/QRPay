// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : CompanyService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-01] → [CU-01] → [Company] → [CompanyTests]
// Autor     : [Tesista]
// Fecha     : 2026
using QRPayments.CompanyService.Domain.Enums;

namespace QRPayments.CompanyService.Domain.Entities;

public class Company
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string BCPAccountNumber { get; set; } = string.Empty;
    public string RUC { get; set; } = string.Empty;
    public CompanyStatus Status { get; set; } = CompanyStatus.Active;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Branch> Branches { get; set; } = new List<Branch>();
}
