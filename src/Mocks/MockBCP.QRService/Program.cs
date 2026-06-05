// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : MockBCP.QRService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-00] → [CU-00] → [MockQR] → [IntegrationTests]
// Autor     : [Tesista]
// Fecha     : 2026
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new() { Title = "MockBCP - QRService", Version = "v1" }));
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
