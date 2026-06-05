// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : UserService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-03] → [CU-03] → [UserStatus] → [UserTests]
// Autor     : [Tesista]
// Fecha     : 2026
namespace QRPayments.UserService.Domain.Enums;

public enum UserStatus
{
    Active = 1,
    Inactive = 2,
    Locked = 3
}
