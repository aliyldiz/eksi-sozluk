using System.Diagnostics;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EksiSozluk.Common.Infrastructure;

public static class QueueFactory 
{
    public static void SendMessageToExchange(string exchangeName, string exchangeType, string queueName, object obj)
    {
        var channel = CreateBasicConsumer().
            EnsureExchange(exchangeName, exchangeType).
            EnsureQueue(queueName, exchangeName).
            Model;
        
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));
        
        channel.BasicPublish(exchangeName, queueName, null, body);
    }
    
    public static EventingBasicConsumer CreateBasicConsumer()
    {
        var factory = new ConnectionFactory() { HostName = SozlukConstants.RabbitMqHost };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        
        return new EventingBasicConsumer(channel);
    }
    
    public static EventingBasicConsumer EnsureExchange(this EventingBasicConsumer consumer, 
        string exchangeName, string exchangeType = SozlukConstants.DefaultExchangeType)
    {
        consumer.Model.ExchangeDeclare(exchangeName, exchangeType, false, false);
        
        return consumer;
    }
    
    public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer consumer, 
        string queueName, string exchangeName)
    {
        consumer.Model.QueueDeclare(queueName, false, false, false, null);
        
        consumer.Model.QueueBind(queueName, exchangeName, queueName);
        
        return consumer;
    }
    
    public static EventingBasicConsumer Receive<T>(this EventingBasicConsumer consumer, Action<T> act)
    {
        consumer.Received += (m, eventArgs) => 
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var model = JsonSerializer.Deserialize<T>(message);

            act(model);
            consumer.Model.BasicAck(eventArgs.DeliveryTag, false);
        };

        return consumer;
    }

    public static EventingBasicConsumer StartConsuming(this EventingBasicConsumer consumer, string queueName)
    {
        consumer.Model.BasicConsume(queue: queueName,
            autoAck: false,
            consumer: consumer);

        return consumer;
    }
}