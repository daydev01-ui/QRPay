# Proyecto  : Sistema de Gestión de Cobros QR — BCP
# Servicio  : CompanyService
# Iteración : Fase 3 — Construcción, Iteración 1
# Autor     : [Tesista]
# Fecha     : 2026
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/QRPayments.CompanyService/QRPayments.CompanyService.csproj", "src/Services/QRPayments.CompanyService/"]
COPY ["shared/QRPayments.Shared.Contracts/QRPayments.Shared.Contracts.csproj", "shared/QRPayments.Shared.Contracts/"]
COPY ["shared/QRPayments.Shared.Infrastructure/QRPayments.Shared.Infrastructure.csproj", "shared/QRPayments.Shared.Infrastructure/"]
RUN dotnet restore "src/Services/QRPayments.CompanyService/QRPayments.CompanyService.csproj"
COPY . .
WORKDIR "/src/src/Services/QRPayments.CompanyService"
RUN dotnet build "QRPayments.CompanyService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QRPayments.CompanyService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QRPayments.CompanyService.dll"]
