using BKS.ConsoleWorker.Requests;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BKS.ConsoleWorker.HostedServices;

public class MainHostedService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly RandomWorker _randomWorker = new();

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Log.Information("Hosted service started");

        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(2));

        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
       _ = _randomWorker.DoRandomWork();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Log.Information("Hosted service stopped");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}