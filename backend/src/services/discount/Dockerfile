FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5003

ENV ASPNETCORE_URLS=http://+:80

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

COPY ["backend/src/services/discount/Discount.Api/Discount.Api.csproj", "services/discount/Discount.Api/"]
COPY ["backend/src/services/discount/Discount.Infrastructure/Discount.Infrastructure.csproj", "services/discount/Discount.Infrastructure/"]
COPY ["backend/src/services/discount/Discount.Core/Discount.Core.csproj", "services/discount/Discount.Core/"]
COPY ["backend/src/services/discount/Discount.Application/Discount.Application.csproj", "services/discount/Discount.Application/"]
COPY ["backend/src/libs/Neith.Core/Neith.Core.csproj", "libs/Neith.Core/"]
COPY ["backend/src/libs/Neith.Core.Infras/Neith.Core.Infras.csproj", "libs/Neith.Core.Infras/"]

RUN dotnet restore "services/discount/Discount.Api/Discount.Api.csproj"

COPY ["backend/src/services/discount/Discount.Api/", "services/discount/Discount.Api/"]
COPY ["backend/src/services/discount/Discount.Infrastructure/", "services/discount/Discount.Infrastructure/"]
COPY ["backend/src/services/discount/Discount.Core/", "services/discount/Discount.Core/"]
COPY ["backend/src/services/discount/Discount.Application/", "services/discount/Discount.Application/"]
COPY ["backend/src/libs/Neith.Core/", "libs/Neith.Core/"]
COPY ["backend/src/libs/Neith.Core.Infras/", "libs/Neith.Core.Infras/"]

WORKDIR "/src/services/discount/Discount.Api"
RUN dotnet build "Discount.Api.csproj"  -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Discount.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.Api.dll"]