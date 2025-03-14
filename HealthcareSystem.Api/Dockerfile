﻿FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HealthcareSystem.Api/HealthcareSystem.Api.csproj", "HealthcareSystem.Api/"]
COPY ["HealthcareSystem.Application/HealthcareSystem.Application.csproj", "HealthcareSystem.Application/"]
COPY ["HealthcareSystem.Core/HealthcareSystem.Core.csproj", "HealthcareSystem.Core/"]
COPY ["HealthcareSystem.Infrastructure/HealthcareSystem.Infrastructure.csproj", "HealthcareSystem.Infrastructure/"]
RUN dotnet restore "HealthcareSystem.Api/HealthcareSystem.Api.csproj"
COPY . .
WORKDIR "/src/HealthcareSystem.Api"
RUN dotnet build "HealthcareSystem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HealthcareSystem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HealthcareSystem.Api.dll"]
