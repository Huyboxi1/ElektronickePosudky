FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ElektronickePosudky.sln", "."]
COPY ["ElektronickePosudky.Api/ElektronickePosudky.Api.csproj", "ElektronickePosudky.Api/"]
COPY ["ElektronickePosudky.Application/ElektronickePosudky.Application.csproj", "ElektronickePosudky.Application/"]
COPY ["ElektronickePosudky.Domain/ElektronickePosudky.Domain.csproj", "ElektronickePosudky.Domain/"]
COPY ["ElektronickePosudky.Infrastructure/ElektronickePosudky.Infrastructure.csproj", "ElektronickePosudky.Infrastructure/"]
COPY ["ElektronickePosudky.Tests/ElektronickePosudky.Tests.csproj", "ElektronickePosudky.Tests/"]

RUN dotnet restore "ElektronickePosudky.sln"

COPY . .

RUN dotnet build "ElektronickePosudky.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElektronickePosudky.Api/ElektronickePosudky.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

COPY --from=publish /app/publish .

RUN mkdir -p /app/Logs

EXPOSE 80 443

HEALTHCHECK --interval=30s --timeout=3s --start-period=40s --retries=3 \
    CMD curl -f http://localhost/swagger/index.html || exit 1

ENTRYPOINT ["dotnet", "ElektronickePosudky.Api.dll"]
