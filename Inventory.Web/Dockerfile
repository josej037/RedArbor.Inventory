# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["Inventory.Web/Inventory.Web.csproj", "Inventory.Web/"]
RUN dotnet restore "Inventory.Web/Inventory.Web.csproj"

COPY . .

WORKDIR "/src/Inventory.Web"
RUN dotnet build "Inventory.Web.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Inventory.Web.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Inventory.Web.dll"]