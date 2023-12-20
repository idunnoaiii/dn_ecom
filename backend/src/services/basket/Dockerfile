FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5002

ENV ASPNETCORE_URLS=http://+:80

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

COPY ["backend/src/services/basket/Basket.Api/Basket.Api.csproj", "services/basket/Basket.Api/"]
COPY ["backend/src/libs/Neith.Core/Neith.Core.csproj", "libs/Neith.Core/"]
COPY ["backend/src/libs/Neith.Core.Infras/Neith.Core.Infras.csproj", "libs/Neith.Core.Infras/"]

RUN dotnet restore "services/basket/Basket.Api/Basket.Api.csproj"

RUN ls

COPY ["backend/src/services/basket/Basket.Api/", "services/basket/Basket.Api/"]
COPY ["backend/src/libs/Neith.Core/", "libs/Neith.Core/"]
COPY ["backend/src/libs/Neith.Core.Infras/", "libs/Neith.Core.Infras/"]

# COPY . .
RUN ls

WORKDIR "/src/services/basket/Basket.Api"
RUN dotnet build "Basket.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Basket.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Basket.Api.dll" ]

