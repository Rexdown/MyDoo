using Microsoft.Extensions.Options;
using MyDoo.DAL.Interfaces;

namespace MyBot;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<Worker> _logger;
    // private readonly IUserDao _userDao;
    // private readonly IOptions<EnvironmentVariables> _options;

    public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        // _userDao = userDao;
        // _options = options;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var telegramBot = new TelegramBotCommunications(scope.ServiceProvider.GetRequiredService<IUserDao>());
            //
            // _logger.LogInformation("Telegram Bot started working");
            telegramBot.StartPolling();
        }
    }
}