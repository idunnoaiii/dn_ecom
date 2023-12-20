FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5002

ENV ASPNETCORE_URLS=http://+:5002

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["backend/src/services/basket/Basket.Api/Basket.Api.csproj", "backend/src/services/basket/Basket.Api/"]
RUN dotnet restore "backend/src/services/basket/Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/backend/src/services/basket/Basket.Api"
RUN dotnet build "Basket.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Basket.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]
