# Proyecto  : Sistema de Gestión de Cobros QR — BCP
# Servicio  : NotificationService
# Iteración : Fase 3 — Construcción, Iteración 1
# Autor     : [Tesista]
# Fecha     : 2026
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/QRPayments.NotificationService/QRPayments.NotificationService.csproj", "src/Services/QRPayments.NotificationService/"]
COPY ["shared/QRPayments.Shared.Contracts/QRPayments.Shared.Contracts.csproj", "shared/QRPayments.Shared.Contracts/"]
COPY ["shared/QRPayments.Shared.Infrastructure/QRPayments.Shared.Infrastructure.csproj", "shared/QRPayments.Shared.Infrastructure/"]
RUN dotnet restore "src/Services/QRPayments.NotificationService/QRPayments.NotificationService.csproj"
COPY . .
WORKDIR "/src/src/Services/QRPayments.NotificationService"
RUN dotnet build "QRPayments.NotificationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QRPayments.NotificationService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QRPayments.NotificationService.dll"]
