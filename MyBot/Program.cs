using MyBot;
using MyBot.Utils;
using Microsoft.EntityFrameworkCore;
using MyDoo.DAL.Interfaces;
using MyDoo.EFDal;
using MyDoo.EFDal.DbContexts;

namespace MyBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<IScopedService, MyScopedService>();
                services.AddHostedService<Worker>();
                
                var generalConfiguration = hostContext.Configuration;
                services.Configure<EnvironmentVariables>(
                    generalConfiguration.GetSection("EnvironmentVariables"));
                
                var npgsqlConfig = generalConfiguration
                    .GetSection("EnvironmentVariables")
                    .Get<EnvironmentVariables>();
                
                services.AddDbContext<NpgsqlContext>(options =>
                {
                    options.UseNpgsql(npgsqlConfig?.NpgsqlConnectionString);
                });

                services.AddScoped<IUserDao, UserDao>();
                
                services.AddHostedService<Worker>();

                var options = new DbContextOptionsBuilder<NpgsqlContext>();
                options.UseNpgsql(npgsqlConfig?.NpgsqlConnectionString);

                var context = new NpgsqlContext(options.Options);
                var users = context.Users;
            });
    }
}