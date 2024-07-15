#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8004
EXPOSE 9004

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Ordering.API/Ordering.API.csproj", "Ordering.API/"]
COPY ["eShopping.ExceptionHandling/eShopping.AspNetCore.ExceptionHandling.csproj", "eShopping.ExceptionHandling/"]
COPY ["eShopping.Exceptions/eShopping.Exceptions.csproj", "eShopping.Exceptions/"]
COPY ["Ordering.Application/Ordering.Application.csproj", "Ordering.Application/"]
COPY ["Ordering.Core/Ordering.Core.csproj", "Ordering.Core/"]
COPY ["Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Ordering.Infrastructure/"]
COPY ["Data.Repositories/Data.Repositories.csproj", "Data.Repositories/"]
RUN dotnet restore "./Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/Ordering.API"
RUN dotnet build "./Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]