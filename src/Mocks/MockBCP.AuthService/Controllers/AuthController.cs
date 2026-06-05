// Proyecto  : Sistema de Gestión de Cobros QR — BCP
// Servicio  : MockBCP.AuthService
// Iteración : Fase 3 — Construcción, Iteración 1
// Trazab.   : [R-00] → [CU-00] → [AuthController] → [IntegrationTests]
// Autor     : [Tesista]
// Fecha     : 2026
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MockBCP.AuthService.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private const string SecretKey = "MockBCPSuperSecretKey2026ForDevelopmentOnly!";

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Username == "admin" && request.Password == "admin123")
        {
            var token = GenerateToken(request.Username, "Admin");
            return Ok(new { token, expiresIn = 3600 });
        }
        return Unauthorized(new { message = "Credenciales inválidas" });
    }

    [HttpPost("validate")]
    public IActionResult Validate([FromBody] ValidateRequest request)
    {
        return Ok(new { valid = true, userId = "mock-user-001" });
    }

    private string GenerateToken(string username, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("iss", "MockBCP.AuthService")
        };
        var token = new JwtSecurityToken(
            issuer: "MockBCP.AuthService",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public record LoginRequest(string Username, string Password);
public record ValidateRequest(string Token);
