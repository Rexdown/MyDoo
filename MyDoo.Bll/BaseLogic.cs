using Microsoft.Extensions.Logging;

namespace MyDoo.Bll
{
    public class BaseLogic
    {
        protected readonly ILogger Logger;

        protected BaseLogic(ILogger<BaseLogic> logger)
        {
            Logger = logger;
        }
    }
}