FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:80

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

COPY ["backend/src/services/discount/Discount.Api/Discount.Api.csproj", "Discount.Api/"]
COPY ["backend/src/services/discount/Discount.Infrastructure/Discount.Infrastructure.csproj", "Discount.Infrastructure/"]
COPY ["backend/src/services/discount/Discount.Core/Discount.Core.csproj", "Discount.Core/"]
COPY ["backend/src/services/discount/Discount.Application/Discount.Application.csproj", "Discount.Application/"]

RUN dotnet restore "discount.Api/discount.Api.csproj"

COPY ["backend/src/services/discount/Discount.Api/", "Discount.Api/"]
COPY ["backend/src/services/discount/Discount.Infrastructure/", "Discount.Infrastructure/"]
COPY ["backend/src/services/discount/Discount.Core/", "Discount.Core/"]
COPY ["backend/src/services/discount/Discount.Application/", "Discount.Application/"]

WORKDIR "/src/discount.Api"
RUN dotnet build "Discount.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Discount.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.Api.dll"]