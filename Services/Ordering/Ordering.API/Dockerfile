#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8004
EXPOSE 9004

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Ordering/Ordering.API/Ordering.API.csproj", "Ordering.API/"]
COPY ["Libraries/eShopping.ExceptionHandling/eShopping.AspNetCore.ExceptionHandling.csproj", "eShopping.ExceptionHandling/"]
COPY ["Libraries/eShopping.Exceptions/eShopping.Exceptions.csproj", "eShopping.Exceptions/"]
COPY ["Services/Ordering/Ordering.Application/Ordering.Application.csproj", "Ordering.Application/"]
COPY ["Services/Ordering/Ordering.Core/Ordering.Core.csproj", "Ordering.Core/"]
COPY ["Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "Ordering.Infrastructure/"]
COPY ["Libraries/Data.IRepositories/Data.IRepositories.csproj", "Data.IRepositories/"]
COPY ["Libraries/Data.Repositories/Data.Repositories.csproj", "Data.Repositories/"]
RUN dotnet restore "./Ordering.API/Ordering.API.csproj"
COPY . .
WORKDIR "/src/Ordering.API"
RUN dotnet build "./Services/Ordering/Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Services/Ordering/Ordering.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]