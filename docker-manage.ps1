# Docker Compose Management Script

param(
    [Parameter(Mandatory=$false)]
    [ValidateSet("up", "down", "logs", "status", "rebuild")]
    [string]$Action = "up"
)

switch ($Action) {
    "up" {
        Write-Host "Starting Transport API infrastructure..." -ForegroundColor Green
        docker-compose up -d
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host ""
            Write-Host "Services started successfully!" -ForegroundColor Green
            Write-Host ""
            Write-Host "Service URLs:" -ForegroundColor Yellow
            Write-Host "  Transport API: http://localhost:8080" -ForegroundColor Cyan
            Write-Host "  Health Check: http://localhost:8080/health" -ForegroundColor Cyan
            Write-Host "  Swagger UI: http://localhost:8080/swagger" -ForegroundColor Cyan
            Write-Host "  RabbitMQ Management: http://localhost:15672 (guest/guest)" -ForegroundColor Cyan
            Write-Host "  PostgreSQL: localhost:5432 (postgres/postgres)" -ForegroundColor Cyan
            Write-Host ""
            Write-Host "Run './docker-manage.ps1 logs' to view logs" -ForegroundColor Yellow
        }
    }
    "down" {
        Write-Host "Stopping Transport API infrastructure..." -ForegroundColor Yellow
        docker-compose down
        Write-Host "Services stopped." -ForegroundColor Green
    }
    "logs" {
        Write-Host "Viewing logs (Press Ctrl+C to exit)..." -ForegroundColor Yellow
        docker-compose logs -f
    }
    "status" {
        Write-Host "Service Status:" -ForegroundColor Yellow
        docker-compose ps
        Write-Host ""
        Write-Host "Health Checks:" -ForegroundColor Yellow
        docker-compose exec transport-api curl -s http://localhost:8080/health | ConvertFrom-Json | Format-List
    }
    "rebuild" {
        Write-Host "Rebuilding and restarting services..." -ForegroundColor Yellow
        docker-compose down
        docker-compose build --no-cache
        docker-compose up -d
        Write-Host "Services rebuilt and started!" -ForegroundColor Green
    }
}