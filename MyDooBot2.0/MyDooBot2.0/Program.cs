using MyDooBot2._0;
using MyDooBot2._0.Utils;
using Microsoft.EntityFrameworkCore;
using MyDoo.DAL.Interfaces;
using MyDoo.EFDal;
using MyDoo.EFDal.DbContexts;

namespace MyDooBot2._0
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
            });
    }
}

// IHost host = Host.CreateDefaultBuilder(args)
//     .ConfigureServices((hostContext, services) =>
//     {
//         var generalConfiguration = hostContext.Configuration;
//         services.Configure<EnvironmentVariables>(
//             generalConfiguration.GetSection("EnvironmentVariables"));
//         
//         services.AddHostedService<Worker>();
//         
//         var npgsqlConfig = generalConfiguration
//             .GetSection("EnvironmentVariables")
//             .Get<EnvironmentVariables>();
//         
//         services.AddDbContext<NpgsqlContext>(options =>
//         {
//             options.UseNpgsql(npgsqlConfig?.NpgsqlConnectionString);
//         });
//         services.AddSingleton<IUserDao, UserDao>();
//     })
//     .Build();

// host.Run();