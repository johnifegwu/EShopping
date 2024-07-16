#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 9002
EXPOSE 8002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Basket/Basket.API/Basket.API.csproj", "Basket.API/"]
COPY ["Libraries/eShopping.ExceptionHandling/eShopping.AspNetCore.ExceptionHandling.csproj", "eShopping.ExceptionHandling/"]
COPY ["Services/Basket/Basket.Application/Basket.Application.csproj", "Basket.Application/"]
COPY ["Services/Basket/Basket.Core/Basket.Core.csproj", "Basket.Core/"]
COPY ["Libraries/Cache.IRepositories/Cache.IRepositories.csproj", "Cache.IRepositories/"]
COPY ["Libraries/Cache.Repositories/Cache.Repositories.csproj", "Cache.Repositories/"]
COPY ["Services/Basket/Basket.Infrastructure/Basket.Infrastructure.csproj", "Basket.Infrastructure/"]
COPY ["Libraries/Cache.UnitOfWork.Redis/Cache.UnitOfWork.csproj", "Cache.UnitOfWork.Redis/"]
RUN dotnet restore "./Services/Basket/Basket.API/Basket.API.csproj"
COPY . .
WORKDIR "/src/Basket.API"
RUN dotnet build "./Services/Basket/Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Services/Basket/Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]