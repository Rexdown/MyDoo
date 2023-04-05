using Microsoft.EntityFrameworkCore;
using MyDoo.DAL.Interfaces;
using MyDoo.EFDal.DbContexts;
using MyDoo.Entities;

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

    public async Task<bool> CheckUserAsync(string email, string password)
    {
        var user = await DbContext.Users
            .FirstOrDefaultAsync(u => 
                u.Email == email
                && u.Password == password);
        
        return user != null;
    }

    public async Task AddUserAsync(User user)
    {
        await DbContext.AddAsync(user);
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