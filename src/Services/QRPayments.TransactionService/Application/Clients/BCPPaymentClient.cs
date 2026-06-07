// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : TransactionService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-05] → [CU-05] → [BCPPaymentClient] → [TransactionTests]
// Autor     : [Tesista]
// Fecha     : 2026
using System.Text;
using System.Text.Json;
using QRPayments.TransactionService.Application.Interfaces;

namespace QRPayments.TransactionService.Application.Clients;

public class BCPPaymentClient : IBCPPaymentClient
{
    private readonly HttpClient _http;
    private readonly ILogger<BCPPaymentClient> _logger;

    public BCPPaymentClient(HttpClient http, ILogger<BCPPaymentClient> logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task<BCPPaymentResult> ProcessPaymentAsync(string qrId, decimal amount, string currency, string idempotencyKey, CancellationToken ct = default)
    {
        var payload = new { qrId, amount, currency, idempotencyKey };
        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _logger.LogInformation("Calling BCP payment service for QR {QrId}", qrId);
        var response = await _http.PostAsync("/api/payments/process", content, ct);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync(ct);
        var result = JsonSerializer.Deserialize<BCPPaymentResultRaw>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? throw new InvalidOperationException("Invalid response from BCP payment service");

        return new BCPPaymentResult(result.TransactionId, result.Status, result.Amount, result.Timestamp, result.BcpReference);
    }

    private record BCPPaymentResultRaw(string TransactionId, string Status, decimal Amount, DateTime Timestamp, string BcpReference);
}
