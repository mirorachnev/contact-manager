using ContactManager.Api.Handlers;
using ContactManager.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddSingleton<IMessageBus, MessageBus>();
builder.Services.AddSingleton<IHandleResponseBusMessages, HandleResponseBusMessages>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();