using Microsoft.AspNetCore.Mvc;
using AiDemoTransport.Services.Messaging;

namespace AiDemoTransport.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IMessagePublisher _messagePublisher;
    private readonly ILogger<TestController> _logger;

    public TestController(IMessagePublisher messagePublisher, ILogger<TestController> logger)
    {
        _messagePublisher = messagePublisher;
        _logger = logger;
    }

    [HttpPost("rabbitmq")]
    public async Task<IActionResult> TestRabbitMQ()
    {
        try
        {
            var testMessage = new
            {
                Message = "Test message from API",
                Timestamp = DateTime.UtcNow,
                TestId = Guid.NewGuid()
            };

            // Test direct queue creation only
            await _messagePublisher.PublishAsync(testMessage, "search.events");

            _logger.LogInformation("Test message published successfully");
            return Ok(new { 
                Success = true, 
                Message = "Test message published to RabbitMQ queue",
                Timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to publish test messages to RabbitMQ");
            return StatusCode(500, new { 
                Success = false, 
                Error = ex.Message,
                Timestamp = DateTime.UtcNow
            });
        }
    }
}