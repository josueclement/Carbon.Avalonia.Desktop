# DI Containers — Host vs WebApplication

> **Q:** With Host adding hosted services like `ApiHostedService` in my case and adding services in general, will the `ApiHostedService` share the same container? If I'm adding a singleton service, will it be accessible in both my Avalonia application and the hosted service?

**Yes and no** — it depends on which container you're looking at.

Your setup has two separate DI containers:

1. **The Host container** (`Program.AppHost.Services`) — built at `Program.cs:25-32`. This is where all your singletons (`INavigationService`, `MainWindowViewModel`, etc.) and the `ApiHostedService` live.
2. **The WebApplication container** (`ApiHostedService.cs:22`) — `WebApplication.CreateSlimBuilder()` creates its own independent `IServiceProvider`. Services registered in the Host container are **not** available inside `_app`.

So right now, if you register a singleton in `ConfigureServices` (the Host), your Avalonia app can resolve it (via `Program.AppHost.Services`), but `ApiHostedService`'s minimal API endpoints **cannot** — they use the WebApplication's own container.

## How to share the container

The simplest fix: inject the services you need directly into `ApiHostedService` via its constructor (the Host resolves those), then pass them to your endpoints manually:

```csharp
public class ApiHostedService(IConfiguration configuration, INavigationService navService) : IHostedService
{
    // ...
    _app.MapGet("/api/nav", () => Results.Ok(navService.SomeData));
}
```

Since `ApiHostedService` is resolved from the Host container, it can receive any singleton registered there via constructor injection. You just can't use the WebApplication's built-in DI to auto-resolve them in endpoint delegates.

Alternatively, if you want the WebApplication to share the exact same container, you'd need to pass the Host's `IServiceProvider` into the `WebApplicationBuilder` — but that's more complex and usually unnecessary for a sidecar API like this.
