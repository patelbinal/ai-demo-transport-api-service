# Transport API - Docker Setup

This document provides instructions for building and running the Transport API using Docker and Docker Compose.

## 🐳 Docker Files Overview

- **`Dockerfile`** - Multi-stage build for the .NET Core API
- **`docker-compose.yml`** - Complete infrastructure setup (API + PostgreSQL + RabbitMQ)
- **`.dockerignore`** - Excludes unnecessary files from Docker build context

## 🚀 Quick Start

### Option 1: Docker Compose (Recommended)
```powershell
# Start all services (API + Database + Message Queue)
docker-compose up -d

# Or use the management script
.\docker-manage.ps1 up
```

### Option 2: Build and Run Manually
```powershell
# Build the Docker image
.\build-docker.ps1

# Start dependencies first
.\setup-infrastructure.ps1

# Run the API container
docker run -p 8080:8080 --name transport-api transport-api:latest
```

## 📊 Service URLs

Once running, access these services:

- **🌐 Transport API**: http://localhost:8080
- **📋 Health Check**: http://localhost:8080/health
- **📚 Swagger UI**: http://localhost:8080/swagger
- **🐰 RabbitMQ Management**: http://localhost:15672 (guest/guest)
- **🐘 PostgreSQL**: localhost:5432 (postgres/postgres)

## 🛠️ Management Commands

```powershell
# Start services
.\docker-manage.ps1 up

# View logs
.\docker-manage.ps1 logs

# Check status
.\docker-manage.ps1 status

# Stop services
.\docker-manage.ps1 down

# Rebuild and restart
.\docker-manage.ps1 rebuild
```

## 🔧 Environment Variables

The Docker setup uses these environment variables:

### Database Connection
- `ConnectionStrings__DefaultConnection`: PostgreSQL connection string
- Host: `postgres` (container name)
- Database: `transport`
- User: `postgres`
- Password: `postgres`

### RabbitMQ Configuration
- `RabbitMQ__HostName`: `rabbitmq` (container name)
- `RabbitMQ__Port`: `5672`
- `RabbitMQ__UserName`: `guest`
- `RabbitMQ__Password`: `guest`

## 📝 Testing the API

### Create a Transport (Triggers RabbitMQ Event)
```bash
curl -X POST http://localhost:8080/api/transports \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Docker Test Bus",
    "type": "Bus",
    "licensePlate": "DTB-001",
    "capacity": 40,
    "description": "Test transport from Docker"
  }'
```

### Check Health
```bash
curl http://localhost:8080/health
```

## 🔍 Monitoring

### View RabbitMQ Events
1. Open http://localhost:15672
2. Login with `guest/guest`
3. Navigate to **Queues** tab
4. Check `transport-created-queue` for published events

### View Logs
```powershell
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f transport-api
```

### Check Container Status
```powershell
docker-compose ps
```

## 🏗️ Docker Image Details

- **Base Image**: `mcr.microsoft.com/dotnet/aspnet:8.0`
- **Build Image**: `mcr.microsoft.com/dotnet/sdk:8.0`
- **Port**: 8080
- **Health Check**: `/health` endpoint
- **User**: Non-root user for security

## 🔄 Database Migrations

The database migrations are applied automatically when the container starts. The Docker Compose setup ensures:

1. PostgreSQL starts first
2. RabbitMQ starts second  
3. API waits for both dependencies to be healthy
4. Migrations are applied on startup

## 🛡️ Production Considerations

- Change default passwords in production
- Use Docker secrets for sensitive data
- Configure proper logging and monitoring
- Set up proper backup strategies for volumes
- Use specific image tags instead of `latest`