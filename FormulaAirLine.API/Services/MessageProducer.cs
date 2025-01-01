using System.Text;
using System.Text.Json;
using FormulaAirLine.API.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FormulaAirLine.API;

public class MessageProducer : IMessageProducer
{
    private readonly IConfiguration _configuration;

    public MessageProducer(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendingMessage<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQ:HostName"],
            UserName = _configuration["RabbitMQ:UserName"],
            Password = _configuration["RabbitMQ:Password"],
            VirtualHost = _configuration["RabbitMQ:VirtualHost"]
        };

        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "bookings", durable: true, exclusive: true);
        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublish(exchange: "", routingKey: "bookings", body: body);
    }
}