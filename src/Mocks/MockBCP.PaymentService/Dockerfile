# Proyecto  : Sistema de Gestión de Cobros QR — BCP
# Servicio  : MockBCP.PaymentService
# Iteración : Fase 3 — Construcción, Iteración 1
# Autor     : [Tesista]
# Fecha     : 2026
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Mocks/MockBCP.PaymentService/MockBCP.PaymentService.csproj", "src/Mocks/MockBCP.PaymentService/"]
RUN dotnet restore "src/Mocks/MockBCP.PaymentService/MockBCP.PaymentService.csproj"
COPY . .
WORKDIR "/src/src/Mocks/MockBCP.PaymentService"
RUN dotnet build "MockBCP.PaymentService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MockBCP.PaymentService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MockBCP.PaymentService.dll"]
