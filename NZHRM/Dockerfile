# =============================================================
# Stage 1: BUILD
#   Uses the full .NET 9 SDK to restore, build and publish
# =============================================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy every .csproj file first so Docker can cache the restore layer.
# If only source code changes (not .csproj), the restore step is skipped on rebuild.
COPY ["NZ.HRM.WebAPI/NZ.HRM.WebAPI.csproj",             "NZ.HRM.WebAPI/"]
COPY ["NZ.HRM.Application/NZ.HRM.Application.csproj",   "NZ.HRM.Application/"]
COPY ["NZ.HRM.Application.Model/NZ.HRM.Application.Model.csproj", "NZ.HRM.Application.Model/"]
COPY ["NZ.HRM.Domain/NZ.HRM.Domain.csproj",             "NZ.HRM.Domain/"]
COPY ["NZ.HRM.Infrastructure/NZ.HRM.Infrastructure.csproj", "NZ.HRM.Infrastructure/"]
COPY ["NZ.HRM.Mapping/NZ.HRM.Mapping.csproj",           "NZ.HRM.Mapping/"]
COPY ["NZ.HRM.Utility/NZ.HRM.Utility.csproj",           "NZ.HRM.Utility/"]

# Restore NuGet packages for the entry-point project (pulls all dependencies)
RUN dotnet restore "NZ.HRM.WebAPI/NZ.HRM.WebAPI.csproj"

# Copy the full source code into the container
COPY . .

# Publish a Release build to /app/publish
WORKDIR "/src/NZ.HRM.WebAPI"
RUN dotnet publish "NZ.HRM.WebAPI.csproj" \
		-c Release \
		-o /app/publish \
		--no-restore

# =============================================================
# Stage 2: RUNTIME
#   Uses the smaller ASP.NET runtime image (no SDK bloat)
# =============================================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Port 8080 is the default HTTP port for ASP.NET Core in containers
EXPOSE 8080

# Copy only the published output from the build stage
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "NZ.HRM.WebAPI.dll"]
