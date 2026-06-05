# Proyecto  : Sistema de Gestión de Cobros QR — BCP
# Servicio  : AuditService
# Iteración : Fase 3 — Construcción, Iteración 1
# Autor     : [Tesista]
# Fecha     : 2026
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/QRPayments.AuditService/QRPayments.AuditService.csproj", "src/Services/QRPayments.AuditService/"]
COPY ["shared/QRPayments.Shared.Contracts/QRPayments.Shared.Contracts.csproj", "shared/QRPayments.Shared.Contracts/"]
COPY ["shared/QRPayments.Shared.Infrastructure/QRPayments.Shared.Infrastructure.csproj", "shared/QRPayments.Shared.Infrastructure/"]
RUN dotnet restore "src/Services/QRPayments.AuditService/QRPayments.AuditService.csproj"
COPY . .
WORKDIR "/src/src/Services/QRPayments.AuditService"
RUN dotnet build "QRPayments.AuditService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QRPayments.AuditService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QRPayments.AuditService.dll"]
