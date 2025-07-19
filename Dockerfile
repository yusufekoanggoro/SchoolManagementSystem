# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Salin file .env ke dalam container runtime
COPY .env .env

# Jalankan aplikasi
# ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]
ENTRYPOINT ["dotnet", "SchoolManagementSystem.dll"]