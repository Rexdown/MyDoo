using Microsoft.Extensions.Logging;
using MyDoo.Bll.Interfaces;
using MyDoo.DAL.Interfaces;
using MyDoo.Entities;

namespace MyDoo.Bll;

public class UserLogic : BaseLogic, IUserLogic
{
    private readonly IUserDao _userDao;

    public UserLogic(
        ILogger<UserLogic> logger,
        IUserDao userDao) 
        : base(logger)
    {
        _userDao = userDao;
    }

    public Task<User> GetUserAsync(int id)
    {
        return _userDao.GetUserAsync(id);
    }

    public Task<User> GetUserAsync(string email)
    {
        return _userDao.GetUserAsync(email);
    }

    public Task<bool> CheckUserAsync(string email, string password)
    {
        return _userDao.CheckUserAsync(email, password);
    }

    public Task AddUserAsync(User user)
    {
        return _userDao.AddUserAsync(user);
    }

    public Task<bool> RemoveUserAsync(int id)
    {
        return _userDao.RemoveUserAsync(id);
    }

    public Task UpdateUserAsync(User user)
    {
        return _userDao.UpdateUserAsync(user);
    }
}