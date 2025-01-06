
using Cybervision.Dapr.DataModels;
using Cybervision.Dapr.Profiles;
using Cybervision.Dapr.Repositories;
using Cybervision.Dapr.Services;
using TodoApi.Middleware;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("Starting up");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddTransient<IJournalEntryRepository, JournalEntryRepository>();
builder.Services.AddTransient<ITodoItemDataService, TodoItemDataService>();
builder.Services.AddTransient<IJournalEntryDataService, JournalEntryDataService>();
builder.Services.AddTransient<IUserDataService, UserDataService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITodoItemService, TodoItemService>();
builder.Services.AddTransient<IJournalEntryService, JournalEntryService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(TodoItemProfile));
builder.Services.AddAutoMapper(typeof(JournalEntryProfile));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

//RegisterClassMaps();

builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Logging.SetMinimumLevel(LogLevel.Debug);
builder.Logging.AddConsole();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

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

//static void RegisterClassMaps()
//{
//    if (!BsonClassMap.IsClassMapRegistered(typeof(UserDocument)))
//    {
//        BsonClassMap.RegisterClassMap<UserDocument>(cm =>
//        {
//            cm.AutoMap();
//            cm.MapMember(c => c.SearchKey)
//                .SetIsRequired(false)
//                .SetSerializer(new SearchKeyAliasSerializer("Username"));
//        });
//    }
//}