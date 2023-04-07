using MyDoo.Entities;
using Task = System.Threading.Tasks.Task;

namespace MyDoo.DAL.Interfaces;

public interface ITaskDao
{
    Task<UserTask> GetUserTaskAsync(int id);
    IEnumerable<UserTask> GetUserTaskList(int userId, DateTime date);
    // IEnumerable<UserTask> GetUserTaskList(int userId, DateTime date, bool important, bool complete);
    Task AddUserTaskAsync(UserTask task);
    Task<bool> RemoveUserTaskAsync(int id);
    Task UpdateUserTaskAsync(UserTask task);
}