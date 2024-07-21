#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 9002
EXPOSE 8002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Basket/Basket.API/Basket.API.csproj", "Services/Basket/Basket.API/"]
COPY ["Libraries/eShopping.ExceptionHandling/eShopping.AspNetCore.ExceptionHandling.csproj", "Libraries/eShopping.ExceptionHandling/"]
COPY ["Libraries/eShopping.Exceptions/eShopping.Exceptions.csproj", "Libraries/eShopping.Exceptions/"]
COPY ["Services/Basket/Basket.Application/Basket.Application.csproj", "Services/Basket/Basket.Application/"]
COPY ["Services/Basket/Basket.Core/Basket.Core.csproj", "Services/Basket/Basket.Core/"]
COPY ["Libraries/Cache.IRepositories/Cache.IRepositories.csproj", "Libraries/Cache.IRepositories/"]
COPY ["Libraries/eShopping.Models/eShopping.Models.csproj", "Libraries/eShopping.Models/"]
COPY ["Services/Basket/Basket.Infrastructure/Basket.Infrastructure.csproj", "Services/Basket/Basket.Infrastructure/"]
COPY ["Libraries/Cache.UnitOfWork.Redis/Cache.UnitOfWork.csproj", "Libraries/Cache.UnitOfWork.Redis/"]
COPY ["Libraries/Cache.Repositories/Cache.Repositories.csproj", "Libraries/Cache.Repositories/"]
RUN dotnet restore "./Services/Basket/Basket.API/Basket.API.csproj"
COPY . .
WORKDIR "/src/Services/Basket/Basket.API"
RUN dotnet build "./Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Basket.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]