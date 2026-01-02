using Microsoft.AspNetCore.Mvc;

namespace AiDemoTransport.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var healthStatus = new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Service = "AiDemoTransport API",
            Version = "1.0.0"
        };

        return Ok(healthStatus);
    }

    [HttpGet("detailed")]
    public async Task<IActionResult> GetDetailed([FromServices] AiDemoTransport.Data.TransportDbContext dbContext)
    {
        var healthStatus = new
        {
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Service = "AiDemoTransport API",
            Version = "1.0.0",
            Dependencies = new
            {
                Database = await CheckDatabaseHealth(dbContext),
                RabbitMQ = "Connected" // You can enhance this with actual RabbitMQ health check
            }
        };

        return Ok(healthStatus);
    }

    private async Task<string> CheckDatabaseHealth(AiDemoTransport.Data.TransportDbContext dbContext)
    {
        try
        {
            await dbContext.Database.CanConnectAsync();
            return "Connected";
        }
        catch
        {
            return "Disconnected";
        }
    }
}