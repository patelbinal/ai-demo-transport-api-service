# Docker Build and Run Scripts

# Build the Docker image
Write-Host "Building Transport API Docker image..." -ForegroundColor Green
docker build -t transport-api:latest .

if ($LASTEXITCODE -eq 0) {
    Write-Host "Docker image built successfully!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Available commands:" -ForegroundColor Yellow
    Write-Host "  docker run -p 8080:8080 transport-api:latest" -ForegroundColor Cyan
    Write-Host "  docker-compose up -d" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Image details:" -ForegroundColor Yellow
    docker images transport-api:latest
} else {
    Write-Host "Docker build failed!" -ForegroundColor Red
    exit 1
}