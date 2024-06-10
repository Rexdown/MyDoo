using System.Text;
using Microsoft.Extensions.Options;
using MyDoo.DAL.Interfaces;
using MyDoo.DataAccess.Service2.MessageHandlers;
using MyDoo.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MyDoo.DataAccess.Service2.Rabbit;

public class Rabbit : BackgroundService
{
    private readonly ITaskDao _taskDao;
    
    private const string queueName = "MyDoo_Queue";

    private readonly ConnectionFactory _connectionFactory;
    private readonly BusOptions _busOptions;
    private readonly ILogger<Rabbit> _logger;
    private IConnection _connection;
    private IModel _channel;

    public Rabbit(IOptions<BusOptions> busOptions, ILogger<Rabbit> logger, ITaskDao taskDao)
    {
        _busOptions = busOptions.Value;
        _logger = logger;
        _taskDao = taskDao;
        
        _connectionFactory = new ConnectionFactory
        {
            HostName = _busOptions.HostName,
            UserName = _busOptions.UserName,
            Password = _busOptions.Password,
            Port = _busOptions.Port,
        };

        CreateBusConnection();
    }

    private void CreateBusConnection()
    {
        _connection = _connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        
        _channel.QueueDeclare(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    private void DefineConsumer()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (_, eventArgs) =>
        {
            var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

            _logger.LogInformation($"Получено сообщение: {content}");

            var messageHandler = new MessageHandler(_taskDao);

            var response = messageHandler.Handle(content);

            var serializedReply = JsonConvert.SerializeObject(response);
            var bytesReply = Encoding.UTF8.GetBytes(serializedReply);

            var props = _channel.CreateBasicProperties();
            props.ReplyTo = queueName;
            props.CorrelationId = eventArgs.BasicProperties.CorrelationId;
            
            _channel.BasicPublish(exchange: "",
                routingKey: props.ReplyTo,
                basicProperties: props,
                body: bytesReply);

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };
        
        _channel.BasicConsume(queueName, false, consumer);
    }

    protected override Task ExecuteAsync(CancellationToken cToken)
    {
        cToken.ThrowIfCancellationRequested();
        
        DefineConsumer();

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        
        base.Dispose();
    }
}