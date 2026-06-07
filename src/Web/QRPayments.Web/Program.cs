// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : QRPayments.Web (Blazor Server)
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

// Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("Gateway", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["GatewayUrl"] ?? "http://localhost:5000");
});

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHealthChecks();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorComponents<QRPayments.Web.Components.App>()
    .AddInteractiveServerRenderMode();
app.MapHealthChecks("/health");

app.Run();
