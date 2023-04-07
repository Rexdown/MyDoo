using Microsoft.Extensions.Logging;
using MyDoo.Bll.Interfaces;
using MyDoo.DAL.Interfaces;
using MyDoo.Entities;

namespace MyDoo.Bll;

public class TaskLogic : BaseLogic, ITaskLogic
{
    private readonly ITaskDao _taskDao;
    
    public TaskLogic(
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

    public IEnumerable<UserTask> GetUserTaskList(int userId, DateTime date)
    {
        return _taskDao.GetUserTaskList(userId, date);
    }

    // public IEnumerable<UserTask> GetUserTaskList(int userId, DateTime date, bool important, bool complete)
    // {
    //     return _taskDao.GetUserTaskList(userId, date, important, complete);
    // }

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