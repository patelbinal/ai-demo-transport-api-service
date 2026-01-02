# PostgreSQL Docker Setup Script
# Run this to start PostgreSQL in Docker

# Stop and remove any existing container
docker stop postgres-transport 2>$null
docker rm postgres-transport 2>$null

# Run PostgreSQL in Docker
docker run --name postgres-transport `
  -e POSTGRES_DB=transport `
  -e POSTGRES_USER=postgres `
  -e POSTGRES_PASSWORD=postgres `
  -p 5432:5432 `
  -d postgres:15

Write-Host "PostgreSQL container started!"
Write-Host "Connection details:"
Write-Host "  Host: localhost"
Write-Host "  Port: 5432"
Write-Host "  Database: transport"
Write-Host "  Username: postgres"
Write-Host "  Password: postgres"
Write-Host ""
Write-Host "Wait a few seconds for PostgreSQL to start, then run:"
Write-Host "  dotnet ef database update"