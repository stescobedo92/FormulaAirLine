using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace FormulaAirLine.API.Services;

public class MessageProducer : IMessageProducer, IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageProducer(IConfiguration configuration)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:HostName"] ?? "localhost",
            Port = int.Parse(configuration["RabbitMQ:Port"] ?? "5672"),
            UserName = configuration["RabbitMQ:UserName"] ?? "guest",
            Password = configuration["RabbitMQ:Password"] ?? "guest",
            VirtualHost = configuration["RabbitMQ:VirtualHost"] ?? "/"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "bookings", durable: true, exclusive: false);
    }

    public void SendingMessage<T>(T message)
    {
        var body = Encoding.UTF8.GetBytes(message is string msg ? msg : JsonSerializer.Serialize(message));

        _channel.BasicPublish(exchange: "", routingKey: "bookings", body: body);
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}