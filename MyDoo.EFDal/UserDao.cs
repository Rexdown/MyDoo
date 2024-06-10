using System.Text;
using Microsoft.EntityFrameworkCore;
using MyDoo.DAL.Interfaces;
using MyDoo.EFDal.DbContexts;
using MyDoo.Entities;
using Newtonsoft.Json;

namespace MyDoo.EFDal;

public class UserDao : BaseDAO, IUserDao
{
    public UserDao(NpgsqlContext context) : base(context)
    {
    }
    
    public async Task<User> GetUserAsync(int id)
    {
        var user = await DbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public async Task<User> GetUserAsync(string email)
    {
        var user = await DbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task<User> GetUserTgAsync(string tgname)
    {
        var user = await DbContext.Users.FirstOrDefaultAsync(u => u.TGName == tgname);
        return user;
    }


    public async Task<int> CheckUserAsync(string email, string password)
    {
        var user = await DbContext.Users
            .FirstOrDefaultAsync(u => 
                u.Email == email
                && u.Password == password);
        
        return user.Id;
    }

    public async Task AddUserAsync(User user)
    {
        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();
    }

    public async Task<bool> RemoveUserAsync(int id)
    {
        var user = await GetUserAsync(id);

        DbContext.Users.Remove(user);
        var entitiesCnt = await DbContext.SaveChangesAsync();
        return entitiesCnt != 0;
    }

    public async Task UpdateUserAsync(User user)
    {
        DbContext.Users.Update(user);
        await DbContext.SaveChangesAsync();
    }
}