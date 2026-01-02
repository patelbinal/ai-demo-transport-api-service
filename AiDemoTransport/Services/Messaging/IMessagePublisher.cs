namespace AiDemoTransport.Services.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message, string queueName) where T : class;
}