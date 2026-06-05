# Proyecto  : Sistema de Gestión de Cobros QR — BCP
# Servicio  : MockBCP.QRService
# Iteración : Fase 3 — Construcción, Iteración 1
# Autor     : [Tesista]
# Fecha     : 2026
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Mocks/MockBCP.QRService/MockBCP.QRService.csproj", "src/Mocks/MockBCP.QRService/"]
RUN dotnet restore "src/Mocks/MockBCP.QRService/MockBCP.QRService.csproj"
COPY . .
WORKDIR "/src/src/Mocks/MockBCP.QRService"
RUN dotnet build "MockBCP.QRService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MockBCP.QRService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MockBCP.QRService.dll"]
