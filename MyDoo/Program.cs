using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyDoo;
using MyDoo.Bll;
using MyDoo.Bll.Interfaces;
using MyDoo.DAL.Interfaces;
using MyDoo.EFDal;
using MyDoo.EFDal.DbContexts;
using MyDoo.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddScoped<IUserDao, UserDao>();
builder.Services.AddScoped<ITaskDao, TaskDao>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<ITaskLogic, TaskLogic>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    var jsonConverter = new JsonStringEnumConverter();
    options.JsonSerializerOptions.Converters.Add(jsonConverter);
});

var config = builder.Configuration
    .GetSection("EnvironmentVariables")
    .Get<EnvironmentVariables>();

var mappingConfig = new MapperConfiguration(mc =>
{
    var mappingProfile = new MappingProfile();
    mc.AddProfile(mappingProfile);
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddDbContext<NpgsqlContext>(options =>
{
    options.UseNpgsql(config?.NpgsqlConnectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

var options = new DbContextOptionsBuilder<NpgsqlContext>();
options.UseNpgsql(config?.NpgsqlConnectionString);
var context = new NpgsqlContext(options.Options);
var users = context.Users.ToList();