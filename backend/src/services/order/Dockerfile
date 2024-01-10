FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5003

ENV ASPNETCORE_URLS=http://+:80

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

COPY ["backend/src/services/order/Order.Api/Order.Api.csproj", "services/order/Order.Api/"]
COPY ["backend/src/services/order/Order.Infrastructure/Order.Infrastructure.csproj", "services/order/Order.Infrastructure/"]
COPY ["backend/src/services/order/Order.Core/Order.Core.csproj", "services/order/Order.Core/"]
COPY ["backend/src/services/order/Order.Application/Order.Application.csproj", "services/order/Order.Application/"]
COPY ["backend/src/libs/Neith.Core/Neith.Core.csproj", "libs/Neith.Core/"]
COPY ["backend/src/libs/Neith.Core.Infras/Neith.Core.Infras.csproj", "libs/Neith.Core.Infras/"]

RUN dotnet restore "services/order/Order.Api/Order.Api.csproj"

COPY ["backend/src/services/order/Order.Api/", "services/order/Order.Api/"]
COPY ["backend/src/services/order/Order.Infrastructure/", "services/order/Order.Infrastructure/"]
COPY ["backend/src/services/order/Order.Core/", "services/order/Order.Core/"]
COPY ["backend/src/services/order/Order.Application/", "services/order/Order.Application/"]
COPY ["backend/src/libs/Neith.Core/", "libs/Neith.Core/"]
COPY ["backend/src/libs/Neith.Core.Infras/", "libs/Neith.Core.Infras/"]

WORKDIR "/src/services/order/Order.Api"
RUN dotnet build "Order.Api.csproj"  -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Order.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Api.dll"]