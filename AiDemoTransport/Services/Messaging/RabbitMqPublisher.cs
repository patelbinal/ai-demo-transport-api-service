using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using AiDemoTransport.Services.Messaging;

namespace AiDemoTransport.Services.Messaging;

public class RabbitMqPublisher : IMessagePublisher
{
    private readonly IConfiguration _configuration;
    public RabbitMqPublisher(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task PublishAsync<T>(T message, string queueName) where T : class
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration.GetValue<string>("RabbitMQ:HostName") ?? "localhost",
            Port = _configuration.GetValue<int>("RabbitMQ:Port", 5672),
            UserName = _configuration.GetValue<string>("RabbitMQ:UserName") ?? "guest",
            Password = _configuration.GetValue<string>("RabbitMQ:Password") ?? "guest",
            VirtualHost = _configuration.GetValue<string>("RabbitMQ:VirtualHost") ?? "/"
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        var options = new System.Text.Json.JsonSerializerOptions
        {
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
            WriteIndented = false
        };
        var body = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(message, options));
        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body: body);
        await Task.CompletedTask;
    }
}