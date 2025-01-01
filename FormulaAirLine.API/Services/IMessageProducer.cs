namespace FormulaAirLine.API.Services;

public interface IMessageProducer
{
    public void SendingMessage<T>(T message);
}