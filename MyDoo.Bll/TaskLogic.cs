using Microsoft.Extensions.Logging;
using MyDoo.Bll.Interfaces;
using MyDoo.Bll.RabbitMq;
using MyDoo.DAL.Interfaces;
using MyDoo.Entities;
using MyDoo.Entities.DTO;
using System.Text.Json;
using Newtonsoft.Json;

namespace MyDoo.Bll;

public class TaskLogic : BaseLogic, ITaskLogic
{
    private readonly ITaskDao _taskDao;

    public TaskLogic(
        ILogger<TaskLogic> logger,
        ITaskDao taskDao,
        IRabbitMqMessageService rabbitMqMessageService) 
        : base(logger, rabbitMqMessageService)
    {
        _taskDao = taskDao;
    }

    public Task<UserTask> GetUserTaskAsync(int id)
    {
        return _taskDao.GetUserTaskAsync(id);
    }

    public async Task<IEnumerable<UserTask>> GetUserTaskListAsync(int userId, DateTime date)
    {
        var dto = new GetUserTasksDTO
        {
            UserId = userId,
            Date = date
        };

        var serializedResponse = (string) await RabbitMqMessageService.SendMessageWithReplyAsync(dto).ConfigureAwait(false);
        var response = JsonConvert.DeserializeObject<IEnumerable<UserTask>>(serializedResponse);
        
        return response;
    }

    public Task AddUserTaskAsync(UserTask task)
    {
        return _taskDao.AddUserTaskAsync(task);
    }

    public Task<bool> RemoveUserTaskAsync(int id)
    {
        return _taskDao.RemoveUserTaskAsync(id);
    }

    public Task UpdateUserTaskAsync(UserTask task)
    {
        return _taskDao.UpdateUserTaskAsync(task);
    }
}