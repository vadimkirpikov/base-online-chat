using ChatAPI.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
}); 
builder.Services.AddSignalR();
builder.Services.AddStackExchangeRedisCache(options =>
{
    var connection = builder.Configuration.GetConnectionString("RedisConnection");
    options.Configuration = connection;
});
var app = builder.Build();
app.MapHub<ChatHub>("/chat");
app.UseCors();
app.Run();