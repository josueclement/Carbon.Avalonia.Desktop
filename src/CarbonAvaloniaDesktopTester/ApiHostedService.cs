using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CarbonAvaloniaDesktopTester;

public class ApiHostedService(IOptions<ApiOptions> options) : IHostedService
{
    private WebApplication? _app;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var apiOptions = options.Value;

        var builder = WebApplication.CreateSlimBuilder();
        builder.WebHost.UseUrls($"http://localhost:{apiOptions.Port}");

        _app = builder.Build();

        _app.MapGet("/", () => Results.Ok(new { apiOptions.Title, Status = "Running" }));
        _app.MapGet("/api/info", () => Results.Ok(new
        {
            apiOptions.Title,
            apiOptions.Port,
            Environment.MachineName,
            StartedAt = DateTime.UtcNow
        }));

        await _app.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_app is not null)
        {
            await _app.StopAsync(cancellationToken);
            await _app.DisposeAsync();
        }
    }
}
