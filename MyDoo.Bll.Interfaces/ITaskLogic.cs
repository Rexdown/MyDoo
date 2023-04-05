using MyDoo.Entities;

namespace MyDoo.Bll.Interfaces;

public interface ITaskLogic
{
    Task<UserTask> GetUserTaskAsync(int id);
    Task<UserTask> GetUserTaskListAsync(int userId, DateTime date, TaskType type, bool important, bool complete);
    Task<UserTask> GetUserTaskListAsync(int userId, DateTime date, bool important, bool complete);
    Task AddUserTaskAsync(UserTask task);
    Task<bool> RemoveUserTaskAsync(int id);
    Task UpdateUserTaskAsync(UserTask task);
}