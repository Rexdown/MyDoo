using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyDoo.Bll.Interfaces;
using MyDoo.EFDal;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace AnimeShop.TelegramBot
{
    public static class TelegramBotCommunications
    {
        private static TelegramBotClient _tgBot;
        private static readonly string _token = "6286192604:AAE_RDgg-5J13NmCyhPTjWqHorc7NJ0qigw";
        private static InlineKeyboardMarkup Keyboard = new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Сгенерировать пароль", "/getpassword")
            }
        });

        public static void StartPolling()
        {
            _tgBot = new TelegramBotClient(_token);
            _tgBot.StartReceiving(Update, Error);

            Console.ReadLine();
        }

        private static async Task Update(ITelegramBotClient tgClient, Update update, CancellationToken token)
        {
            var message = update.Message.Text;
            var chatId = update.Message.Chat.Id;
            var userName = update.Message.Chat.Username;
            Random rnd = new Random();

            switch (message)
            {
                case "/start":
                    await tgClient.SendTextMessageAsync(chatId, "Добро пожаловать!", cancellationToken: token);
                    break;
                case "/getpassword":
                    await tgClient.SendTextMessageAsync(update.Message.Chat.Id, $"Вы {userName}");

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
                    
                    await tgClient.SendTextMessageAsync(update.Message.Chat.Id, resultPassword, cancellationToken: token);
                    break;
                default:
                    await tgClient.SendTextMessageAsync(update.Message.Chat.Id, "Ты че, идиот?");
                    break;
            }
        }

        private static async Task Error(ITelegramBotClient tgClient, Exception ex, CancellationToken token)
        {
            bool result = await tgClient.TestApiAsync(token);
            if (!result)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}