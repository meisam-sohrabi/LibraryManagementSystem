# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Api/LibrarySysApi/LibrarySysApi.csproj", "src/Api/LibrarySysApi/"]
COPY ["src/Core/LibrarySys.Application/LibrarySys.Application.csproj", "src/Core/LibrarySys.Application/"]
COPY ["src/Core/LibrarySys.Domain/LibrarySys.Domain.csproj", "src/Core/LibrarySys.Domain/"]
COPY ["src/Infrastructure/LibrarySys.Identity/LibrarySys.Identity.csproj", "src/Infrastructure/LibrarySys.Identity/"]
COPY ["src/Infrastructure/LibrarySys.Infrastructure/LibrarySys.Infrastructure.csproj", "src/Infrastructure/LibrarySys.Infrastructure/"]
RUN dotnet restore "./src/Api/LibrarySysApi/LibrarySysApi.csproj"
COPY . .
WORKDIR "/src/src/Api/LibrarySysApi"
RUN dotnet build "./LibrarySysApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LibrarySysApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibrarySysApi.dll"]