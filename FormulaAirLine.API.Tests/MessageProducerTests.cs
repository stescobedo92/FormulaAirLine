using FormulaAirLine.API.Services;
using Microsoft.Extensions.Configuration;

namespace FormulaAirLine.API.Tests;

public class MessageProducerTests : IDisposable
{
    private readonly MessageProducer _messageProducer;

    public MessageProducerTests()
    {
        // 1. Load test configuration (appsettings.Test.json)
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Test.json", true)
            .AddEnvironmentVariables()
            .Build();

        // 2. Create the MessageProducer with the loaded config
        _messageProducer = new MessageProducer(config);
    }

    [Fact(DisplayName = "Case 1: Verify default configuration and successful connection")]
    public void Should_Connect_With_Default_Configuration()
    {
        // ARRANGE
        // (Initialization is already done in the constructor.)

        // ACT
        // The connection is made in the constructor of MessageProducer.
        // If there's an issue, it will throw an exception.

        // ASSERT
        // If no exception was thrown, the connection succeeded.
        Assert.True(true, "Connected successfully using default or test configuration.");
    }

    [Fact(DisplayName = "Case 2: Send string message to the 'bookings' queue")]
    public void Should_Send_String_Message()
    {
        // ARRANGE
        var testMessage = "Hello RabbitMQ";

        // ACT
        var exception = Record.Exception(() => _messageProducer.SendingMessage(testMessage));

        // ASSERT
        Assert.Null(exception);
    }

    [Fact(DisplayName = "Case 3: Send an object serialized as JSON")]
    public void Should_Send_Object_As_JSON()
    {
        // ARRANGE
        var booking = new
        {
            FlightNumber = "FA123",
            Date = DateTime.Now,
            PassengerName = "John Doe"
        };

        // ACT
        var exception = Record.Exception(() => _messageProducer.SendingMessage(booking));

        // ASSERT
        Assert.Null(exception);
    }

    [Fact(DisplayName = "Case 4: Handling error with invalid configuration")]
    public void Should_Throw_Exception_When_Config_Is_Invalid()
    {
        // ARRANGE
        var invalidConfig = new ConfigurationBuilder()
            .AddInMemoryCollection([new KeyValuePair<string, string?>("RabbitMQ:HostName", "non-existent-host"), new KeyValuePair<string, string?>("RabbitMQ:Port", "12345")])
            .Build();

        // ACT
        Exception thrownException = null;
        try
        {
            using var producer = new MessageProducer(invalidConfig);
        }
        catch (Exception ex)
        {
            thrownException = ex;
        }

        // ASSERT
        Assert.NotNull(thrownException);
        Assert.IsAssignableFrom<RabbitMQ.Client.Exceptions.BrokerUnreachableException>(thrownException);
    }

    public void Dispose()
    {
        _messageProducer?.Dispose();
    }
}