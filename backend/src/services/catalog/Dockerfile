FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:80

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

COPY ["backend/src/services/catalog/Catalog.Api/Catalog.Api.csproj", "Catalog.Api/"]
COPY ["backend/src/services/catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "Catalog.Infrastructure/"]
COPY ["backend/src/services/catalog/Catalog.Core/Catalog.Core.csproj", "Catalog.Core/"]
COPY ["backend/src/services/catalog/Catalog.Application/Catalog.Application.csproj", "Catalog.Application/"]

RUN dotnet restore "Catalog.Api/Catalog.Api.csproj"

COPY ["backend/src/services/catalog/Catalog.Api/", "Catalog.Api/"]
COPY ["backend/src/services/catalog/Catalog.Infrastructure/", "Catalog.Infrastructure/"]
COPY ["backend/src/services/catalog/Catalog.Core/", "Catalog.Core/"]
COPY ["backend/src/services/catalog/Catalog.Application/", "Catalog.Application/"]

WORKDIR "/src/Catalog.Api"
RUN dotnet build "Catalog.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Catalog.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Api.dll"]
