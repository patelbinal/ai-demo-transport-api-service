# RabbitMQ Docker Setup Script
# Run this to start RabbitMQ in Docker

# Stop and remove any existing container
docker stop rabbitmq-transport 2>$null
docker rm rabbitmq-transport 2>$null

# Run RabbitMQ in Docker with management plugin
docker run --name rabbitmq-transport `
  -e RABBITMQ_DEFAULT_USER=guest `
  -e RABBITMQ_DEFAULT_PASS=guest `
  -p 5672:5672 `
  -p 15672:15672 `
  -d rabbitmq:3.12-management

Write-Host "RabbitMQ container started!"
Write-Host "Connection details:"
Write-Host "  Host: localhost"
Write-Host "  Port: 5672 (AMQP)"
Write-Host "  Management UI: http://localhost:15672"
Write-Host "  Username: guest"
Write-Host "  Password: guest"
Write-Host ""
Write-Host "Wait a few seconds for RabbitMQ to start, then access the management UI at:"
Write-Host "  http://localhost:15672"
Write-Host ""
Write-Host "The following exchange and queue will be created automatically:"
Write-Host "  Exchange: transport.events (Topic)"
Write-Host "  Queue: transport-created-queue"
Write-Host "  Routing Key: transport.created"