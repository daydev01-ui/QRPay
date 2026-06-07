// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : ReportService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-06] → [CU-06] → [Report] → [ReportTests]
// Autor     : [Tesista]
// Fecha     : 2026
using QRPayments.ReportService.Domain.Enums;

namespace QRPayments.ReportService.Domain.Entities;

public class Report
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public ReportType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    public string GeneratedBy { get; set; } = string.Empty;
    public string? FilePath { get; set; }
}
