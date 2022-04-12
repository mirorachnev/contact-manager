using ContactManager.Common;
using ContactManager.DataProvider.DbData;
using ContactManager.DataProvider.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

if (Debugger.IsAttached)
{
    configurationBuilder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: false);
}

IConfiguration configuration = configurationBuilder
    .AddEnvironmentVariables()
    .Build();

IHostBuilder hostBuilder = Host.CreateDefaultBuilder()
    .ConfigureServices(services => 
        services
        .AddSingleton<IMessageBus, MessageBus>()
        .AddDbContext<ContactManagerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(Constants.ContactManagerDbConnectionStringName))));

IHost host = hostBuilder.Build();

var context = host.Services.GetService<ContactManagerDbContext>();
context?.Database.Migrate();

var messageBus = host.Services.GetService<IMessageBus>();
messageBus?.ConnectAsync().Wait();

await host.RunAsync();
