// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : QRPayments.Web (Razor Pages)
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-00] → [CU-00] → [WebProgram] → [IntegrationTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient("Gateway", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["GatewayUrl"] ?? "http://localhost:5000");
});

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
    });

builder.Services.AddAuthorization();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Error");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapHealthChecks("/health");

app.Run();
