using MyDoo.Entities;
using Task = System.Threading.Tasks.Task;

namespace MyDoo.DAL.Interfaces;

public interface IUserDao
{
    Task<User> GetUserAsync(int id);
    Task<User> GetUserAsync(string email);
    Task<bool> CheckUserAsync(string email, string password);
    Task AddUserAsync(User user);
    Task<bool> RemoveUserAsync(int id);
    Task UpdateUserAsync(User user);
}