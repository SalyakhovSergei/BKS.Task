using BKS.ConsoleWorker.HostedServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

Log.Information($"Console service started {DateTime.UtcNow}");

Log.Logger = new LoggerConfiguration()
    .CreateLogger();

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsetings.json", false, true)
    .Build();

await Host.CreateDefaultBuilder(args)
    .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(config, "Serilog"))
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<MainHostedService>();
    })
    .RunConsoleAsync();
