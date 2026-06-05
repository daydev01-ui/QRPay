# Proyecto  : Sistema de Gestión de Cobros QR — BCP
# Servicio  : MockBCP.AuthService
# Iteración : Fase 3 — Construcción, Iteración 1
# Autor     : [Tesista]
# Fecha     : 2026
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Mocks/MockBCP.AuthService/MockBCP.AuthService.csproj", "src/Mocks/MockBCP.AuthService/"]
RUN dotnet restore "src/Mocks/MockBCP.AuthService/MockBCP.AuthService.csproj"
COPY . .
WORKDIR "/src/src/Mocks/MockBCP.AuthService"
RUN dotnet build "MockBCP.AuthService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MockBCP.AuthService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MockBCP.AuthService.dll"]
