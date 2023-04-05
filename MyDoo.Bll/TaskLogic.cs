using Microsoft.Extensions.Logging;
using MyDoo.Bll.Interfaces;
using MyDoo.DAL.Interfaces;
using MyDoo.Entities;

namespace MyDoo.Bll;

public class TaskLogic : BaseLogic, ITaskLogic
{
    private readonly ITaskDao _taskDao;
    
    protected TaskLogic(
        ILogger<TaskLogic> logger,
        ITaskDao taskDao) 
        : base(logger)
    {
        _taskDao = taskDao;
    }

    public Task<UserTask> GetUserTaskAsync(int id)
    {
        return _taskDao.GetUserTaskAsync(id);
    }

    public Task<UserTask> GetUserTaskListAsync(int userId, DateTime date, TaskType type, bool important, bool complete)
    {
        return _taskDao.GetUserTaskListAsync(userId, date, type, important, complete);
    }

    public Task<UserTask> GetUserTaskListAsync(int userId, DateTime date, bool important, bool complete)
    {
        return _taskDao.GetUserTaskListAsync(userId, date, important, complete);
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