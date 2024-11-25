
//using Microsoft.Extensions.DependencyInjection;
using TodoApi.Data;
using TodoApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("Starting up");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<TodoItemService>();//builder.Configuration);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Logging.SetMinimumLevel(LogLevel.Debug);
builder.Logging.AddConsole();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

try
{
    app.MapControllers();
    app.UseRouting();
    app.UseCustomAuthenticationMiddleware();
    app.UseHttpsRedirection();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

app.Run();
