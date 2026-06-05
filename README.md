# Sistema de Gestión de Cobros QR — BCP Bolivia

**Cliente:** Banco de Crédito de Bolivia S.A.  
**Arquitectura:** Microservicios  
**Metodología:** RUP — Fase 3, Construcción, Iteración 1  
**Año:** 2026

## Levantar el proyecto con un solo comando

```bash
cd docker && docker-compose up --build
```

## Servicios disponibles tras levantar

| Servicio | URL |
|---|---|
| API Gateway | http://localhost:5000 |
| Web (Razor Pages) | http://localhost:5001 |
| CompanyService | http://localhost:5002/swagger |
| UserService | http://localhost:5003/swagger |
| NotificationService | http://localhost:5004/swagger |
| ReportService | http://localhost:5005/swagger |
| AuditService | http://localhost:5006/swagger |
| MockBCP Auth | http://localhost:5010/swagger |
| MockBCP QR | http://localhost:5011/swagger |
| MockBCP Payment | http://localhost:5012/swagger |

## Credenciales de prueba (Mock)

- **Usuario:** admin  
- **Contraseña:** admin123  
- **Endpoint:** POST http://localhost:5010/api/auth/login

## Estructura del proyecto

```
QRPayments/
├── src/
│   ├── Gateway/QRPayments.Gateway/     → Ocelot API Gateway
│   ├── Services/                        → 5 microservicios propios
│   ├── Web/QRPayments.Web/             → Frontend Razor Pages
│   └── Mocks/                          → Simulaciones BCP API
├── shared/                             → Librerías compartidas
├── tests/                              → Tests unitarios e integración
├── docker/docker-compose.yml           → Orquestación Docker
└── QRPayments.sln                      → Solution Visual Studio
```
