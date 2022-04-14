using ContactManager.Api.Handlers;
using ContactManager.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

var devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder => {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddSingleton<IMessageBus, MessageBus>();
builder.Services.AddSingleton<IHandleResponseBusMessages, HandleResponseBusMessages>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(devCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();