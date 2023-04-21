using MyDoo.Entities;

namespace MyDoo.Bll.Interfaces
{
    public interface IUserLogic
    {
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string email);
        Task<User> GetUserTgAsync(string tgname);
        Task<bool> CheckUserAsync(string email, string password);
        Task AddUserAsync(User user);
        Task<bool> RemoveUserAsync(int id);
        Task UpdateUserAsync(User user);
    }
}