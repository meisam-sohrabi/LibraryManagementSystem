# ---- Build Stage ----
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj (relative to build context root)
COPY src/api/LibrarySysApi/*.csproj ./src/api/LibrarySysApi/
RUN dotnet restore ./src/api/LibrarySysApi/LibrarySysApi.csproj

# Copy everything from the repo root
COPY . .

# Build the project
WORKDIR /app/src/api/LibrarySysApi
RUN dotnet build LibrarySysApi.csproj -c Release -o /app/build

# ---- Publish Stage ----
FROM build AS publish
RUN dotnet publish LibrarySysApi.csproj -c Release -o /app/publish /p:UseAppHost=false

# ---- Runtime Stage ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibrarySysApi.dll"]