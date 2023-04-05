using Microsoft.EntityFrameworkCore;
using MyDoo.EFDal.DbContexts;
using MyDoo.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration.GetSection("EnvironmentVariables").Get<EnvironmentVariables>();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<NpgsqlContext>(options =>
{
    options.UseNpgsql(config?.NpgsqlConnectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// app.Run();

var options = new DbContextOptionsBuilder<NpgsqlContext>();
options.UseNpgsql(config?.NpgsqlConnectionString);
var context = new NpgsqlContext(options.Options);
var users = context.Users.ToList();
