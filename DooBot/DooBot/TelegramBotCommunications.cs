using Microsoft.Extensions.Options;
using MyDoo.DAL.Interfaces;
using DooBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace DooBot;

public class TelegramBotCommunications
{
    private readonly TelegramBotClient _tgBot;
    private static readonly string _token = "6286192604:AAE_RDgg-5J13NmCyhPTjWqHorc7NJ0qigw";
    
    private readonly IUserDao _userDao;
    private readonly IDictionary<string, string> _credentials;
    private readonly ILogger<TelegramBotCommunications> _logger;

    public TelegramBotCommunications(IUserDao userDao, IOptions<EnvironmentVariables> options)
    {
        _userDao = userDao;
        _logger = new Logger<TelegramBotCommunications>(LoggerFactory.Create(loggerBuilder =>
        {
            loggerBuilder.SetMinimumLevel(LogLevel.Trace).AddConsole();
        }));
        _credentials = new Dictionary<string, string>();
        _tgBot = new TelegramBotClient(_token);
    }

    public void StartPolling()
    {
        _tgBot.StartReceiving(Update, Error);

        Console.ReadLine();
    }

    private void Update(ITelegramBotClient tgClient, Update update, CancellationToken token)
    {
        var message = update.Message.Text;
        var chatId = update.Message.Chat.Id;
        var userName = update.Message.Chat.Username;
        Random rnd = new Random();

        switch (message)
        {
            case "/start":
                tgClient.SendTextMessageAsync(chatId, "Добро пожаловать!", cancellationToken: token);
                break;
            case "/getpassword":
                tgClient.SendTextMessageAsync(update.Message.Chat.Id, $"Вы {userName}");

                // await tgClient.SendTextMessageAsync(update.Message.Chat.Id, $"Вы {userName}");

                // if (result != null)
                // {
                //     await tgClient.SendTextMessageAsync(update.Message.Chat.Id, $"Вы {result.Name}");
                // }
                // else
                // {
                //     await tgClient.SendTextMessageAsync(update.Message.Chat.Id, $"Вы лох ебучий");
                // }
                byte[] bytes = new byte[25];
                rnd.NextBytes(bytes);
                byte[] randomPassword = bytes.Select(b => (byte)(b % 26 + 97)).ToArray();
                string resultPassword = "";
                
                foreach (var c in randomPassword)
                {
                    bool rndUpper = rnd.Next(0, 2) == 1;
                    resultPassword += rndUpper ? (char) (c - 32) : (char) c;
                }
                
                tgClient.SendTextMessageAsync(update.Message.Chat.Id, resultPassword, cancellationToken: token);
                break;
            default:
                tgClient.SendTextMessageAsync(update.Message.Chat.Id, "Ты че, идиот?");
                break;
        }
    }

    private void Error(ITelegramBotClient tgClient, Exception ex, CancellationToken token)
    {

    }
}