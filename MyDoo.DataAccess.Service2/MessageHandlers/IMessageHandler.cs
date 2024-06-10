namespace MyDoo.DataAccess.Service2.MessageHandlers;

public interface IMessageHandler
{
    object Handle(string messageContent);
}