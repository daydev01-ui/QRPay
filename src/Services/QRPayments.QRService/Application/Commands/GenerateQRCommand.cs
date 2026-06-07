// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : QRService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-04] → [CU-04] → [GenerateQRCommand] → [QRTests]
// Autor     : [Tesista]
// Fecha     : 2026
using MediatR;
using QRCoder;
using QRPayments.QRService.Application.Interfaces;
using QRPayments.QRService.Domain.Entities;

namespace QRPayments.QRService.Application.Commands;

public record GenerateQRCommand(
    Guid CompanyId,
    decimal Amount,
    string Currency,
    int ExpiresInMinutes = 30) : IRequest<QRCode>;

public class GenerateQRCommandHandler : IRequestHandler<GenerateQRCommand, QRCode>
{
    private readonly IQRRepository _repo;

    public GenerateQRCommandHandler(IQRRepository repo) => _repo = repo;

    public async Task<QRCode> Handle(GenerateQRCommand request, CancellationToken cancellationToken)
    {
        // Build a unique code string containing all payment data
        var codeData = $"QRP|{request.CompanyId}|{request.Amount:F2}|{request.Currency}|{DateTime.UtcNow:yyyyMMddHHmmssfff}|{Guid.NewGuid():N}";

        // Generate PNG via QRCoder
        using var qrGenerator = new QRCodeGenerator();
        var qrData = qrGenerator.CreateQrCode(codeData, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new PngByteQRCode(qrData);
        var pngBytes = qrCode.GetGraphic(10);
        var imageBase64 = Convert.ToBase64String(pngBytes);

        var entity = new QRCode
        {
            CompanyId = request.CompanyId,
            Amount = request.Amount,
            Currency = request.Currency,
            Code = codeData,
            ImageBase64 = imageBase64,
            ExpiresAt = DateTime.UtcNow.AddMinutes(request.ExpiresInMinutes)
        };

        await _repo.AddAsync(entity, cancellationToken);
        await _repo.SaveChangesAsync(cancellationToken);
        return entity;
    }
}
