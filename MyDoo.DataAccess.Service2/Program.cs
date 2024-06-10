using Microsoft.EntityFrameworkCore;
using MyDoo.DAL.Interfaces;
using MyDoo.DataAccess.Service2;
using MyDoo.DataAccess.Service2.Rabbit;
using MyDoo.EFDal;
using MyDoo.EFDal.DbContexts;
using MyDoo.Entities;

var hostBuilder = Host.CreateApplicationBuilder(args);

var configuration = hostBuilder.Configuration;
var config = configuration
    .GetSection("EnvironmentVariables")
    .Get<EnvironmentVariables>();

hostBuilder.Services.Configure<BusOptions>(configuration.GetSection(BusOptions.SectionName));

hostBuilder.Services.AddScoped<IScopedService, MyScopedService>();
hostBuilder.Services.AddDbContext<NpgsqlContext>(options =>
{
    options.UseNpgsql(config?.NpgsqlConnectionString);
});
hostBuilder.Services.AddScopedSingleton<ITaskDao, TaskDao>();
hostBuilder.Services.AddHostedService<Rabbit>();

var app = hostBuilder.Build();
app.Run();

var options = new DbContextOptionsBuilder<NpgsqlContext>();
options.UseNpgsql(config?.NpgsqlConnectionString);
var context = new NpgsqlContext(options.Options);
var users = context.Users.ToList();