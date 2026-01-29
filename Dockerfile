# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["DiagramAnalyzer.Web/DiagramAnalyzer.Web.csproj", "DiagramAnalyzer.Web/"]
COPY ["DiagramAnalyzer.Core/DiagramAnalyzer.Core.csproj", "DiagramAnalyzer.Core/"]
RUN dotnet restore "DiagramAnalyzer.Web/DiagramAnalyzer.Web.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/DiagramAnalyzer.Web"
RUN dotnet build "DiagramAnalyzer.Web.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "DiagramAnalyzer.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy published app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "DiagramAnalyzer.Web.dll"]
