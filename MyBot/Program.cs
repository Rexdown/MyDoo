using MyBot;
using Microsoft.EntityFrameworkCore;
using MyDoo.DAL.Interfaces;
using MyDoo.EFDal;
using MyDoo.EFDal.DbContexts;
using MyDoo.Entities;
using EnvironmentVariables = MyBot.Utils.EnvironmentVariables;

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
                services.Configure<BusOptions>(
                    generalConfiguration.GetSection(BusOptions.SectionName));
                
                var npgsqlConfig = generalConfiguration
                    .GetSection("EnvironmentVariables")
                    .Get<EnvironmentVariables>();
                
                services.AddDbContext<NpgsqlContext>(options =>
                {
                    options.UseNpgsql(npgsqlConfig?.NpgsqlConnectionString);
                });

                services.AddScopedSingleton<IUserDao, UserDao>();

                var options = new DbContextOptionsBuilder<NpgsqlContext>();
                options.UseNpgsql(npgsqlConfig?.NpgsqlConnectionString);

                var context = new NpgsqlContext(options.Options);
                var users = context.Users;
            });
    }
}