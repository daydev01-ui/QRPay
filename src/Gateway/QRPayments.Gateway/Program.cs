// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : Gateway (Ocelot)
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-00] → [CU-00] → [Program] → [IntegrationTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["Auth:Authority"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new()
        {
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Auth:Issuer"],
        };
    });

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");
app.UseSerilogRequestLogging();

await app.UseOcelot();

app.Run();
