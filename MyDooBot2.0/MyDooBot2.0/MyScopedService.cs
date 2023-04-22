using MyDoo.DAL.Interfaces;

namespace MyDooBot2._0;

public class MyScopedService: IScopedService
{
    private readonly ILogger<Worker> _logger;
    private readonly IUserDao _userDao;
    // private readonly IOptions<EnvironmentVariables> _options;

    public MyScopedService(ILogger<Worker> logger, IUserDao userDao)
    {
        _logger = logger;
        _userDao = userDao;
        // _options = options;
    }
}

public interface IScopedService
{
}