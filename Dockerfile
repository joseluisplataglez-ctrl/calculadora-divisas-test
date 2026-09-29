FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled-extra AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copiar archivos de proyecto (.csproj) individualmente
# Esto se hace PRIMERO para que el 'dotnet restore' se cachee
COPY ["CurrencyCalculatorSystem.Presentation/CurrencyCalculatorSystem.Presentation.csproj", "CurrencyCalculatorSystem.Presentation/"]
COPY ["CurrencyCalculatorSystem.Application/CurrencyCalculatorSystem.Application.csproj", "CurrencyCalculatorSystem.Application/"]
COPY ["CurrencyCalculatorSystem.Domain/CurrencyCalculatorSystem.Domain.csproj", "CurrencyCalculatorSystem.Domain/"]
COPY ["CurrencyCalculatorSystem.Infrastructure/CurrencyCalculatorSystem.Infrastructure.csproj", "CurrencyCalculatorSystem.Infrastructure/"]

RUN dotnet restore "CurrencyCalculatorSystem.Presentation/CurrencyCalculatorSystem.Presentation.csproj"

COPY . .
WORKDIR /src/CurrencyCalculatorSystem.Presentation
RUN dotnet build "CurrencyCalculatorSystem.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENTRYPOINT ["dotnet", "CurrencyCalculatorSystem.Presentation.dll"]