#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QuickOrderPedido.Api/QuickOrderPedido.Api.csproj", "QuickOrderPedido.Api/"]
COPY ["QuickOrderPedido.Application/QuickOrderPedido.Application.csproj", "QuickOrderPedido.Application/"]
COPY ["QuickOrderPedido.Domain/QuickOrderPedido.Domain.csproj", "QuickOrderPedido.Domain/"]
COPY ["QuickOrderPedido.Infra.Gateway/QuickOrderPedido.Infra.Gateway.csproj", "QuickOrderPedido.Infra.Gateway/"]
COPY ["QuickOrderPedido.Infra.MQ/QuickOrderPedido.Infra.MQ.csproj", "QuickOrderPedido.Infra.MQ/"]
COPY ["QuickOrderPedido.IoC/QuickOrderPedido.IoC.csproj", "QuickOrderPedido.IoC/"]
COPY ["QuickOrderPedido.Tests/QuickOrderPedido.Tests.csproj", "QuickOrderPedido.Tests/"]

RUN dotnet restore "./QuickOrderPedido.Api/./QuickOrderPedido.Api.csproj"
COPY . .
WORKDIR "/src/QuickOrderPedido.Api"
RUN dotnet build "./QuickOrderPedido.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QuickOrderPedido.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuickOrderPedido.Api.dll"]