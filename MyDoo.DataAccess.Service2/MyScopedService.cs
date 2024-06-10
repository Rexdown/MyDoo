using MyDoo.DAL.Interfaces;

namespace MyDoo.DataAccess.Service2;

public class MyScopedService : IScopedService
{
    private readonly ILogger<Rabbit.Rabbit> _logger;
    private readonly ITaskDao _taskDao;

    public MyScopedService(ILogger<Rabbit.Rabbit> logger, ITaskDao taskDao)
    {
        _logger = logger;
        _taskDao = taskDao;
    }
}

public interface IScopedService
{
}