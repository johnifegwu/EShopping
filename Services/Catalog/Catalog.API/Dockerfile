#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8001
EXPOSE 9001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Catalog/Catalog.API/Catalog.API.csproj", "Catalog.API/"]
COPY ["Libraries/eShopping.ExceptionHandling/eShopping.AspNetCore.ExceptionHandling.csproj", "eShopping.ExceptionHandling/"]
COPY ["Services/Catalog/Catalog.Application/Catalog.Application.csproj", "Catalog.Application/"]
COPY ["Services/Catalog/Catalog.Core/Catalog.Core.csproj", "Catalog.Core/"]
COPY ["Libraries/Data.IRepositories/Data.IRepositories.csproj", "Data.IRepositories/"]
COPY ["Libraries/Data.Repositories/Data.Repositories.csproj", "Data.Repositories/"]
COPY ["Services/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "Catalog.Infrastructure/"]
RUN dotnet restore "./Services/Catalog/Catalog.API/Catalog.API.csproj"
COPY . .
WORKDIR "/src/Catalog.API"
RUN dotnet build "./Services/Catalog/Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Services/Catalog/Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]