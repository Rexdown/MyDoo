namespace MyDoo.Extensions.RabbitMq;

public interface IRabbitMqMessageService
{
    void SendMessage(object message);

    void SendMessage(string message);

    Task<object> SendMessageWithReplyAsync(object message);

    Task<object> SendMessageWithReplyAsync(string message);
}