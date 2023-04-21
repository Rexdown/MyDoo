using Microsoft.Extensions.Options;
using MyDoo.DAL.Interfaces;
using DooBot.Utils;

namespace DooBot;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IUserDao _userDao;
    private readonly IOptions<EnvironmentVariables> _options;

    public Worker(ILogger<Worker> logger, IUserDao userDao, IOptions<EnvironmentVariables> options)
    {
        _logger = logger;
        _userDao = userDao;
        _options = options;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var tgBot = new TelegramBotCommunications(_userDao, _options);
        tgBot.StartPolling();
    }
}