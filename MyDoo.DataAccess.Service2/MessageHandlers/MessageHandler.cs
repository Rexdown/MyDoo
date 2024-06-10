using MyDoo.DAL.Interfaces;
using MyDoo.Entities.DTO;
using Newtonsoft.Json;

namespace MyDoo.DataAccess.Service2.MessageHandlers;

public class MessageHandler : IMessageHandler
{
    private readonly ITaskDao _taskDao;

    public MessageHandler(ITaskDao taskDao)
    {
        _taskDao = taskDao;
    }
    
    public object Handle(string messageContent)
    {
        var deserializedMessage = JsonConvert.DeserializeObject<GetUserTasksDTO>(messageContent);

        return _taskDao.GetUserTaskList(deserializedMessage.UserId, deserializedMessage.Date);
    }
}