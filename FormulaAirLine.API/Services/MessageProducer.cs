namespace FormulaAirLine.API;

using System.Text;
using RabbitMQ.Client.Exceptions;
using System.Text.Json;
using FormulaAirLine.API.Services;
using RabbitMQ.Client;

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
            HostName = _configuration["RabbitMQ:HostName"] ?? "localhost",
            UserName = _configuration["RabbitMQ:UserName"] ?? "guest",
            Password = _configuration["RabbitMQ:Password"] ?? "guest",
            VirtualHost = _configuration["RabbitMQ:VirtualHost"] ?? "/"
        };

        using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "bookings", durable: true, exclusive: false);

        var body = message is string messageString 
                ? Encoding.UTF8.GetBytes(messageString) 
                : Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        channel.BasicPublish(exchange: "", routingKey: "bookings", body: body);
    }
}