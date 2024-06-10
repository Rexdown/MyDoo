using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyDoo.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MyDoo.Bll.RabbitMq;

public class RabbitMqMessageService : IRabbitMqMessageService
{
    private const string queueName = "MyDoo_Queue";
    private readonly BusOptions _busOptions;
    private readonly ConcurrentDictionary<string, TaskCompletionSource<object>> _tasksMap = new();
    private readonly ILogger<RabbitMqMessageService> _logger;

    private readonly IConnectionFactory _connectionFactory;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqMessageService(IOptions<BusOptions> busOptions, ILogger<RabbitMqMessageService> logger)
    {
        _busOptions = busOptions.Value;
        _logger = logger;
        
        _connectionFactory = new ConnectionFactory
        {
            HostName = _busOptions.HostName,
            UserName = _busOptions.UserName,
            Password = _busOptions.Password,
            Port = _busOptions.Port,
        };

        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        
        _channel.QueueDeclare(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (_, args) =>
        {
            var content = Encoding.UTF8.GetString(args.Body.ToArray());
            
            _logger.LogInformation($"Получен ответ: {content}");

            if (!_tasksMap.TryRemove(args.BasicProperties.CorrelationId, out var tcs)) return;

            if (tcs is not null)
            {
                tcs.TrySetResult(content);
            }
        };

        _channel.BasicConsume(queueName, true, consumer);
    }
    
    public void SendMessage(object message)
    {
        var serializedMessage = JsonSerializer.Serialize(message);
        
        SendMessage(serializedMessage);
    }

    public void SendMessage(string message)
    {
        var convertedMessage = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "",
            routingKey: queueName,
            body: convertedMessage);
    }

    public async Task<object> SendMessageWithReplyAsync(object message)
    {
        var serializedMessage = JsonSerializer.Serialize(message);

        return await SendMessageWithReplyAsync(serializedMessage);
    }

    public Task<object> SendMessageWithReplyAsync(string message)
    {
        var props = _channel.CreateBasicProperties();
        var correlationId = Guid.NewGuid().ToString();

        props.CorrelationId = correlationId;
        props.ReplyTo = queueName;
        
        var convertedMessage = Encoding.UTF8.GetBytes(message);
        var tcs = new TaskCompletionSource<object>();

        _tasksMap.TryAdd(correlationId, tcs);
        _channel.BasicPublish(exchange: "",
            routingKey: queueName,
            basicProperties: props,
            body: convertedMessage);

        return tcs.Task;
    }
}