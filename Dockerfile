#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["L.GastosProdutos.API/L.GastosProdutos.API.csproj", "L.GastosProdutos.API/"]
COPY ["L.GastosProdutos.Core/L.GastosProdutos.Core.csproj", "L.GastosProdutos.Core/"]
RUN dotnet restore "./L.GastosProdutos.API/L.GastosProdutos.API.csproj"
COPY . .
WORKDIR "/src/L.GastosProdutos.API"
RUN dotnet build "./L.GastosProdutos.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./L.GastosProdutos.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --chmod=0755 entrypoint.sh ./
ENTRYPOINT ["./entrypoint.sh"]