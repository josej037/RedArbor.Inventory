# Build
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src
COPY . .
RUN dotnet restore Inventory.Api/Inventory.Api.csproj
RUN dotnet publish Inventory.Api/Inventory.Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Inventory.Api.dll"]