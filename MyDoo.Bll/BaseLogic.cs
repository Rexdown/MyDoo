using Microsoft.Extensions.Logging;
using MyDoo.Bll.RabbitMq;

namespace MyDoo.Bll
{
    public class BaseLogic
    {
        protected readonly ILogger Logger;
        protected readonly IRabbitMqMessageService RabbitMqMessageService;

        protected BaseLogic(ILogger<BaseLogic> logger, IRabbitMqMessageService rabbitMqMessageService)
        {
            Logger = logger;
            RabbitMqMessageService = rabbitMqMessageService;
        }
    }
}