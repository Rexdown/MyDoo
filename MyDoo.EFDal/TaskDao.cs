using Microsoft.EntityFrameworkCore;
using MyDoo.DAL.Interfaces;
using MyDoo.EFDal.DbContexts;
using MyDoo.Entities;

namespace MyDoo.EFDal;

public class TaskDao : BaseDAO, ITaskDao
{
    public TaskDao(NpgsqlContext context) : base(context)
    {
    }

    public async Task<UserTask> GetUserTaskAsync(int id)
    {
        var task = await DbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        return task;
    }

    public IEnumerable<UserTask> GetUserTaskList(int userId, DateTime date)
    {
        var tasks =  DbContext.Tasks.Where(t =>
            t.Id == userId
            && t.Date == date).ToList();
        return tasks;
    }
    
    // public IEnumerable<UserTask> GetUserTaskList(int userId, DateTime date, bool important, bool complete)
    // {
    //     var tasks =  DbContext.Tasks.Where(t =>
    //         t.Id == userId
    //         && t.Date == date
    //         && t.Important == important
    //         && t.Complete == complete).ToList();
    //     return tasks;
    // }

    public async Task AddUserTaskAsync(UserTask task)
    {
        await DbContext.AddAsync(task);
        await DbContext.SaveChangesAsync();
    }

    public async Task<bool> RemoveUserTaskAsync(int id)
    {
        var task = await GetUserTaskAsync(id);

        DbContext.Tasks.Remove(task);
        var entitiesCnt = await DbContext.SaveChangesAsync();
        return entitiesCnt != 0;
    }

    public async Task UpdateUserTaskAsync(UserTask task)
    {
        DbContext.Tasks.Update(task);
        await DbContext.SaveChangesAsync();
    }
}    
    

