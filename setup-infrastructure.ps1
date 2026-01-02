# Complete Infrastructure Setup Script
# Starts both PostgreSQL and RabbitMQ containers

Write-Host "Setting up Transport API infrastructure..." -ForegroundColor Green
Write-Host ""

# PostgreSQL Setup
Write-Host "Starting PostgreSQL..." -ForegroundColor Yellow
docker stop postgres-transport 2>$null
docker rm postgres-transport 2>$null

docker run --name postgres-transport `
  -e POSTGRES_DB=transport `
  -e POSTGRES_USER=postgres `
  -e POSTGRES_PASSWORD=postgres `
  -p 5432:5432 `
  -d postgres:15

# RabbitMQ Setup  
Write-Host "Starting RabbitMQ..." -ForegroundColor Yellow
docker stop rabbitmq-transport 2>$null
docker rm rabbitmq-transport 2>$null

docker run --name rabbitmq-transport `
  -e RABBITMQ_DEFAULT_USER=guest `
  -e RABBITMQ_DEFAULT_PASS=guest `
  -p 5672:5672 `
  -p 15672:15672 `
  -d rabbitmq:3.12-management

Write-Host ""
Write-Host "Infrastructure setup complete!" -ForegroundColor Green
Write-Host ""
Write-Host "PostgreSQL:" -ForegroundColor Cyan
Write-Host "  Host: localhost:5432"
Write-Host "  Database: transport"
Write-Host "  User: postgres / Password: postgres"
Write-Host ""
Write-Host "RabbitMQ:" -ForegroundColor Cyan  
Write-Host "  AMQP: localhost:5672"
Write-Host "  Management UI: http://localhost:15672"
Write-Host "  User: guest / Password: guest"
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "1. Wait 10-15 seconds for services to start"
Write-Host "2. Run: dotnet ef database update"
Write-Host "3. Run: dotnet run"
Write-Host "4. Test transport creation to see RabbitMQ events!"
Write-Host ""
Write-Host "Monitor events in RabbitMQ Management UI:" -ForegroundColor Magenta
Write-Host "  Exchange: transport.events"
Write-Host "  Queue: transport-created-queue"