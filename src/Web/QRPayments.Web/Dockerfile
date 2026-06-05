FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Web/QRPayments.Web/QRPayments.Web.csproj", "src/Web/QRPayments.Web/"]
COPY ["shared/QRPayments.Shared.Contracts/QRPayments.Shared.Contracts.csproj", "shared/QRPayments.Shared.Contracts/"]
RUN dotnet restore "src/Web/QRPayments.Web/QRPayments.Web.csproj"
COPY . .
WORKDIR "/src/src/Web/QRPayments.Web"
RUN dotnet build "QRPayments.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QRPayments.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QRPayments.Web.dll"]
