# Proyecto  : Sistema de Gestión de Cobros QR — BCP
# Servicio  : Gateway
# Iteración : Fase 3 — Construcción, Iteración 1
# Autor     : [Tesista]
# Fecha     : 2026
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Gateway/QRPayments.Gateway/QRPayments.Gateway.csproj", "src/Gateway/QRPayments.Gateway/"]
RUN dotnet restore "src/Gateway/QRPayments.Gateway/QRPayments.Gateway.csproj"
COPY . .
WORKDIR "/src/src/Gateway/QRPayments.Gateway"
RUN dotnet build "QRPayments.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QRPayments.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QRPayments.Gateway.dll"]
